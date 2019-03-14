using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Windows.ApplicationModel;
using System.Xml.Linq;
using System.Reflection;
using System.Diagnostics;
using System.Runtime.Versioning;

namespace msix.catalog.lib
{
    public class ThisAppVersionInfo
    {
        public static string PublisherDisplayName => GetSafePublisherDisplayName();

        private static string GetSafePublisherDisplayName()
        {
            if (OSVersionHelper.WindowsVersionHelper.HasPackageIdentity)
            {
                return Package.Current.PublisherDisplayName;
            }
            return "Not Packaged";
        }

        public static string InstallLocation => System.Reflection.Assembly.GetCallingAssembly().Location;
        public static string VersionString => GetSafePackageVersion();

        private static string GetSafePackageVersion()
        {
            var result = "0";
            if (OSVersionHelper.WindowsVersionHelper.HasPackageIdentity)
            {
                result = string.Format("{0}.{1}.{2}.{3}",
                                                           Package.Current.Id.Version.Major,
                                                           Package.Current.Id.Version.Minor,
                                                           Package.Current.Id.Version.Build,
                                                           Package.Current.Id.Version.Revision);

            }
            else
            {
                var v = Assembly.GetExecutingAssembly().GetName().Version;
                result = string.Format("{0}.{1}.{2}.{3}", v.Major, v.Minor, v.Build, v.Revision);
            }
            return result;
        }

        public static string MyVersion => Assembly.GetCallingAssembly().GetName().FullName;

        public static string Metadata => GetSafeMetadata();
        private static string GetSafeMetadata()
        {
            if (OSVersionHelper.WindowsVersionHelper.HasPackageIdentity)
            {
                var path = Path.Combine(Package.Current.InstalledLocation.Path, "AppxManifest.xml");
                XDocument doc = XDocument.Load(path, LoadOptions.None);
                var xname = XNamespace.Get("http://schemas.microsoft.com/developer/appx/2015/build");
                var metadata = doc.Descendants(xname + "Metadata").First();
                var res = new StringBuilder();
                foreach (XElement n in metadata.Elements())
                {
                    res.Append(n.Attribute("Name").Value + ":");
                    res.Append(n.Attribute("Value")?.Value);
                    res.Append(n.Attribute("Version")?.Value + "\r\n");
                }
                return res.ToString();
            }
            return string.Empty;
        }

        public static string StoreInfo =>GetStoreInfo();
        public static string GetStoreInfo()
        {
            var res = "Store API not available ";
            if (Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.Services.Store.StoreContext"))
            {
                var ctx = Windows.Services.Store.StoreContext.GetDefault();
                var prodTask = ctx.GetStoreProductForCurrentAppAsync().AsTask().Result;

                if (prodTask == null)
                {
                    res = "Can't get product from context";
                }
                else if (prodTask.Product == null)
                {
                    res = "Product is null:" + prodTask.ExtendedError.Message;
                }
                else
                {
                    res = prodTask?.Product?.LinkUri?.ToString();
                }
            }
            return res;
        }

        public static DateTimeOffset InstalledOn  
        {
            get
            {
                DateTimeOffset installed;
                try
                {
                    installed = Package.Current.InstalledDate;
                }
                catch (Exception)
                {
                    installed = DateTime.MinValue;
                }
                return installed;
            }
        }

        public static string DotNetFlavor => typeof(string).Assembly.Location;

        public static string SignatureKind => OSVersionHelper.WindowsVersionHelper.HasPackageIdentity ? Package.Current.SignatureKind.ToString() : "";

        private static AppInstallerInfo GetSafeAppInstallerInfo()
        {
            AppInstallerInfo result = null;
            try
            {
                if (OSVersionHelper.WindowsVersionHelper.IsWindows10October2018OrGreater &&
                          OSVersionHelper.WindowsVersionHelper.HasPackageIdentity &&
                          Windows.Foundation.Metadata.ApiInformation.IsMethodPresent("Windows.ApplicationModel.Package", "GetAppInstallerInfo"))
                {
                    result =  Package.Current.GetAppInstallerInfo();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }
            return result;
        }


        public static string AppInstallerUri
        {
            get
            {
                if (SignatureKind == "Developer")
                {
                    var aiInfo = GetSafeAppInstallerInfo();
                    if (aiInfo != null)
                    {
                        return aiInfo.Uri.ToString();
                    }
                    else
                    {
                        return "AppInstaller info not available";
                    }
                }
                else
                {
                    return "unkown";
                }
            }
        }

        public static string InstallerKind
        {
            get
            {
                string result = "File";
                if (SignatureKind=="Store")
                {
                    result = "Store";
                }
                else if (SignatureKind=="Developer")
                {
                    if (OSVersionHelper.WindowsVersionHelper.IsWindows10October2018OrGreater &&
                        OSVersionHelper.WindowsVersionHelper.HasPackageIdentity)
                    {
                        var aiInfo = GetSafeAppInstallerInfo();
                        if (aiInfo != null)
                        {
                            result = "AppInstaller";
                        }
                    }
                    else
                    {
                        result = "Sideload";
                    }
                } 
                return result;
            }
        }

        public static string GetDeploymentType()
        {
            var result = "Not Packaged";
            if (OSVersionHelper.WindowsVersionHelper.HasPackageIdentity)
            {
                result = $"Packaged from {ThisAppVersionInfo.InstallerKind}";
            }
            else
            {
                if (ThisAppVersionInfo.InstallLocation.Contains(".dotnet"))
                {
                    result = "NuGet global tool";
                }
            }
            return result;
        }

        public static string ProductVersion => FileVersionInfo.GetVersionInfo(Assembly.GetEntryAssembly().Location).ProductVersion;

        public static string GetDotNetInfo()
        {
            string result = "";

            var framework = Assembly
                            .GetEntryAssembly()?
                            .GetCustomAttribute<TargetFrameworkAttribute>()?
                            .FrameworkName;

            bool IsCore = framework.ToLowerInvariant().Contains("core");
            var runTimeDir = new FileInfo(typeof(string).Assembly.Location);
            var entryDir = new FileInfo(Assembly.GetEntryAssembly().Location); 
            bool IsSelfContaied = runTimeDir.DirectoryName == entryDir.DirectoryName;

            if (IsCore)
            {
                result += "NET CORE - ";
                if (IsSelfContaied)
                {
                    result += "SCD";
                }
                else
                {
                    result += "FDD";
                }
            }
            else
            {
                result = "NET FRAMEWORK";
            }

            return result;
        }
    }
}
