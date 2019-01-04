using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using System.Xml.Linq;
using Windows.ApplicationModel;
using Windows.UI.Xaml.Media.Imaging;

namespace msix_catalog_app.ViewModels
{
    public class AboutViewModel
    {
        public string PublisherDisplayName => GetSafePublisherDisplayName();

        private string GetSafePublisherDisplayName()
        {
            if (ExecutionMode.IsRunningAsUwp())
            {
                return Package.Current.PublisherDisplayName;
            }
            return "Not Packaged";
        }

        public string InstallLocation => System.Reflection.Assembly.GetExecutingAssembly().Location;

        public string VersionString => GetSafePackageVersion();

        private string GetSafePackageVersion()
        {
            var result = "0";
            if (ExecutionMode.IsRunningAsUwp())
            {
                result = string.Format("{0}.{1}.{2}.{3}",
                                                           Package.Current.Id.Version.Major,
                                                           Package.Current.Id.Version.Minor,
                                                           Package.Current.Id.Version.Build,
                                                           Package.Current.Id.Version.Revision);

            }
            return result;
        }

        public string MyVersion => this.GetType().AssemblyQualifiedName;

        public string Metadata => GetSafeMetadata();


        private string GetSafeMetadata()
        {
            if (ExecutionMode.IsRunningAsUwp())
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

        public string StoreInfo => GetStoreInfo();

        public static string GetStoreInfo()
        {
            var res = "Store API not available ";
            if (Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.Services.Store.StoreContext"))
            {
                var ctx = Windows.Services.Store.StoreContext.GetDefault();
                var lic = ctx.GetStoreProductForCurrentAppAsync().AsTask().Result;


                if (lic == null)
                {
                    res = "License is null";
                }
                else if (lic.Product == null)
                {
                    res = "License Product is null:" + lic.ExtendedError.Message;
                }
                else
                {
                    res = lic.Product.LinkUri.ToString();
                }
            }
            return res;
        }

        public string InstalledOn
        {
            get
            {
                return SafeInstalledOn();
            }
        }

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
    }
}
