using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Input;
using msix.catalog.app.Mvvm;
using msix.catalog.lib;

namespace msix.catalog.app.ViewModels
{
    public class PackageInfoViewModel : BindableBase
    {
        public ICommand OpenCommand { get; private set; }
        public ICommand ViewManifestCommand { get; private set; }
        public ICommand OpenFolderCommand { get; private set; }
        public ICommand CopyToClipboardCommand { get; private set; }

        public ICommand NavigateToInstallLocationCommand { get; private set; }

        public PackageInfoViewModel() : this(
            new PackageInfo())
        {
        }

        public PackageInfoViewModel(PackageInfo packageInfo)
        {
            PackageInfo = packageInfo;
            OpenCommand = new DelegateCommand<PackageInfoViewModel>(OpenApp, (o) => { return !string.IsNullOrWhiteSpace(packageInfo.PFN); } );
            ViewManifestCommand = new DelegateCommand<PackageInfoViewModel>(ViewManifest);
            OpenFolderCommand = new DelegateCommand<PackageInfoViewModel>(OpenFolder);
            NavigateToInstallLocationCommand = new DelegateCommand<PackageInfoViewModel>(NavigateToInstallLocation, (o) => { return packageInfo.AppInstallerUri != null; });
            CopyToClipboardCommand = new DelegateCommand<PackageInfoViewModel>(CopyToClipboard);
        }

        private void NavigateToInstallLocation(PackageInfoViewModel p)
        {
            Uri appInstallerUri = new Uri(PackageInfo.AppInstallerUri);
            string folderUri = $"{appInstallerUri.Scheme}://{appInstallerUri.DnsSafeHost}";
            for (int i = 0; i < appInstallerUri.Segments.Length-1; i++)
            {
                folderUri += appInstallerUri.Segments[i];
            }
            ProcessStartInfo psi = new ProcessStartInfo()
            {
                UseShellExecute = true,
                FileName = folderUri
            };
            Process.Start(psi);
        }

        public PackageInfo PackageInfo { get; private set; }

        public bool IsSideload => PackageInfo.SignatureKind == "Developer";

        public void OpenApp(PackageInfoViewModel package)
        {
            uint res = PackageActivator.StartApp(package.PackageInfo.PFN);

            App.TelemetryClient.TrackEvent("OpenApp",
                new Dictionary<string, string> { { "AppToOpen", package.PackageInfo.PFN }, { "opened", (res>0).ToString()} },
                null);
        }
       
        public void ViewManifest(PackageInfoViewModel package)
        {
            var manifestPath = Path.Combine(package.PackageInfo.InstallLocation, "AppxManifest.xml");
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

        public void OpenFolder(PackageInfoViewModel package)
        {
            ProcessStartInfo psi = new ProcessStartInfo()
            {
                UseShellExecute = true,
                FileName = package.PackageInfo.InstallLocation
            };
            Process.Start(psi);
            App.TelemetryClient.TrackEvent("OpenFolder",
                new Dictionary<string, string> { { "FolderToOpen", package.PackageInfo.InstallLocation } },
                null);
        }

        public void CopyToClipboard(PackageInfoViewModel package)
        {
            StringBuilder ClipboardText = new StringBuilder();
            ClipboardText.AppendLine(package.PackageInfo.Id);
            ClipboardText.AppendLine(package.PackageInfo.Version);
            ClipboardText.AppendLine (package.PackageInfo.Architecture);
            ClipboardText.AppendLine(package.PackageInfo.Author);
            ClipboardText.AppendLine(package.PackageInfo.InstalledDate.ToString());
            ClipboardText.AppendLine(package.PackageInfo.InstallLocation);
            ClipboardText.AppendLine(package.PackageInfo.PackageFullName);
            ClipboardText.AppendLine(package.PackageInfo.PackageName);

            Clipboard.SetText(ClipboardText.ToString());
        }
    }
}
