using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Windows.ApplicationModel;
using System.Xml.Linq;
using System.Reflection;

namespace msix_catalog_lib
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

        public static string StoreInfo => GetStoreInfo();

        public static string GetStoreInfo()
        {
            var res = "Store API not available ";
            //if (Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.Services.Store.StoreContext"))
            //{
            //    var ctx = Windows.Services.Store.StoreContext.GetDefault();
            //    var lic = ctx.GetStoreProductForCurrentAppAsync().AsTask().Result;

            //    if (lic == null)
            //    {
            //        res = "License is null";
            //    }
            //    else if (lic.Product == null)
            //    {
            //        res = "License Product is null:" + lic.ExtendedError.Message;
            //    }
            //    else
            //    {
            //        res = lic.Product.LinkUri.ToString();
            //    }
            //}
            return res;
        }

        public static string InstalledOn => SafeInstalledOn();

        private static string SafeInstalledOn()
        {
            DateTimeOffset installed;
            try
            {
                installed = Package.Current.InstalledDate;
            }
            catch (Exception)
            {
                installed = DateTime.Now;
            }
            return installed.ToLocalTime().ToString();
        }

        public static string DotNetFlavor => GetDotNetFlavor();

        private static string GetDotNetFlavor()
        {
            return typeof(string).Assembly.Location;
        }
    }
}
