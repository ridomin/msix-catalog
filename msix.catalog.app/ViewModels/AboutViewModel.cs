using msix.catalog.lib;
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

namespace msix.catalog.app.ViewModels
{
    public class AboutViewModel
    {
        public string PublisherDisplayName => ThisAppVersionInfo.PublisherDisplayName;
        public string InstallLocation => ThisAppVersionInfo.InstallLocation;
        public string VersionString => ThisAppVersionInfo.VersionString;
        public string MyVersion => ThisAppVersionInfo.MyVersion;
        public string Metadata => ThisAppVersionInfo.Metadata;
        public string StoreInfo => ThisAppVersionInfo.StoreInfo;
        public string InstalledOn => ThisAppVersionInfo.InstalledOn;
        public string DotNetFlavor => ThisAppVersionInfo.DotNetFlavor;
    }
}
