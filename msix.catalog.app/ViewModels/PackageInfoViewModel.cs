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
using System.Xml.Linq;

namespace msix.catalog.app.ViewModels
{
    public class PackageInfoViewModel : BindableBase
    {
        PackageInfo _packageInfo;
        public ICommand OpenCommand { get; private set; }
        public ICommand ViewManifestCommand { get; private set; }
        public ICommand OpenFolderCommand { get; private set; }

        public PackageInfoViewModel() : this(
            new PackageInfo() { PackageName = "Test Package", Author = "Rido", PFN = "MyPFN" })
        {

        }

        public PackageInfoViewModel(PackageInfo packageInfo)
        {
            _packageInfo = packageInfo;
            OpenCommand = new DelegateCommand<object>(OpenApp, (o) => { return true; });
            ViewManifestCommand = new DelegateCommand<object>(ViewManifest, (o) => { return true; });
            OpenFolderCommand = new DelegateCommand<object>(OpenFolder, (o) => { return true; });
        }

        public PackageInfo PackageInfo
        {
            get
            {
                return _packageInfo;
            }
            private set
            {
                _packageInfo = value;
            }
        }
               
        public void OpenApp(object package)
        {
            var pi = package as PackageInfoViewModel;
            uint res = PackageActivator.StartApp(pi.PackageInfo.PFN);

            App.TelemetryClient.TrackEvent("OpenApp",
                new Dictionary<string, string> { { "AppToOpen", pi.PackageInfo.PFN }, { "opened", (res>0).ToString()} },
                null);
        }
       
        public void ViewManifest(object package)
        {
            var pi = package as PackageInfoViewModel;
            var manifestPath = Path.Combine(pi.PackageInfo.InstallLocation, "AppxManifest.xml");
            Uri manifestUri = new Uri(manifestPath, UriKind.Absolute);
            ProcessStartInfo psi = new ProcessStartInfo()
            {
                UseShellExecute = true,
                FileName = manifestUri.AbsoluteUri
            };
            Process.Start(psi);
            App.TelemetryClient.TrackEvent("ViewManifest",
                new Dictionary<string, string> { { "ManifestToOpen", manifestPath } },
                null);
        }

        public void OpenFolder(object package)
        {
            var pi = package as PackageInfoViewModel;
            ProcessStartInfo psi = new ProcessStartInfo()
            {
                UseShellExecute = true,
                FileName = pi.PackageInfo.InstallLocation
            };
            Process.Start(psi);
            App.TelemetryClient.TrackEvent("OpenFolder",
                new Dictionary<string, string> { { "FolderToOpen", pi.PackageInfo.InstallLocation } },
                null);
        }

    }
}
