using Humanizer;
using msix.catalog.app.Mvvm;
using msix.catalog.lib;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Threading;
using System.Xml.Linq;

namespace msix.catalog.app.ViewModels
{
    public class AboutViewModel
    {
        public ICommand NavigateToGitHubCommand { get; private set; }

        public AboutViewModel()
        {
            NavigateToGitHubCommand = new DelegateCommand<AboutViewModel>(NavigateToGitHub, (o) => { return true; });
        }

        private void NavigateToGitHub(AboutViewModel obj)
        {
            ProcessStartInfo psi = new ProcessStartInfo()
            {
                UseShellExecute = true,
                FileName = this.GitHubProjectUrl
            };
            Process.Start(psi);
        }
        public string GitHubProjectUrl => "https://github.com/ridomin/msix-catalog";
        public string PublisherDisplayName => ThisAppVersionInfo.PublisherDisplayName;
        public string InstallLocation => ThisAppVersionInfo.InstallLocation;
        public string VersionString => ThisAppVersionInfo.VersionString;
        public string MyVersion => ThisAppVersionInfo.MyVersion;
        public string Metadata => ThisAppVersionInfo.Metadata;
        public string StoreInfo => ThisAppVersionInfo.StoreInfo;
        public string InstalledOn => ThisAppVersionInfo.InstalledOn.Humanize();
        public string DotNetFlavor => ThisAppVersionInfo.DotNetFlavor;
        public string InstalledFrom => ThisAppVersionInfo.InstalledFrom;
        public string SignatureKind => ThisAppVersionInfo.SignatureKind;
    }
}
