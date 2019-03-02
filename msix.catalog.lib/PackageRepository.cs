using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using Windows.ApplicationModel;
using Windows.Management.Deployment;

namespace msix.catalog.lib
{
    public class PackageRepository
    {
        public static async Task<IList<PackageInfo>> LoadAllInstalledAppsAsync()
        {
            return await Task.Run(() =>
            {
                var mgr = new PackageManager();
            //var allPackages = mgr.FindPackagesForUserWithPackageTypes(CurrentUserSid.Get(), (PackageTypes)(PackageType.All & ~PackageType.Bundle));
            var allPackages = mgr.FindPackagesForUser(CurrentUserSid.Get());

                return allPackages.Select(p =>
                {
                    return new PackageInfo
                    {
                        Id = p.Id?.Name,
                        LogoUri = GetSafeLogo(p),
                        PackageName = p.Id.FullName,
                        PFN = p.GetFirstAppUserModelId(),
                        Author = p.Id?.Publisher,
                        SignatureKind = p.SignatureKind.ToString(),
                        Architecture = p.Id?.Architecture.ToString(),
                        AppInstallerUri = GetSafeInstallUri(p),
                        IsFramework = p.IsFramework,
                        InstalledDate = p.InstalledDate.UtcDateTime,
                        InstallLocation = p.GetStagedPackagePathByFullName(),
                        Version = $"{p.Id.Version.Major}.{p.Id.Version.Minor}.{p.Id.Version.Build}.{p.Id.Version.Revision}"
                    };
                }).ToList();
            });
        }

        private static string GetSafeLogo(Package p)
        {
            var loc = p.GetStagedPackagePathByFullName();

            if (string.IsNullOrWhiteSpace(loc))
            {
                return string.Empty;
            }

            var manifestPath = Path.Combine(loc, "AppxManifest.xml");
            if (!File.Exists(manifestPath))
            {
                string msg = $"Package {p.Id.FullName} location is not set. Unable to find the manifest.";
                System.Diagnostics.Debug.WriteLine(msg);
                // App.TelemetryClient.TrackTrace(msg);
                return string.Empty;
            }

            var doc = XDocument.Load(manifestPath);
            var xname = XNamespace.Get("http://schemas.microsoft.com/appx/manifest/foundation/windows10");
            var logoNode = doc.Descendants(xname + "Logo")?.FirstOrDefault();
            if (logoNode != null)
            {
                string probe = loc + "\\" + logoNode.Value;
                FileInfo fi = new FileInfo(probe);
                if (fi.Exists)
                {
                    return fi.FullName;
                }

                if (fi.Directory.Exists)
                {
                    var probes = Directory.EnumerateFiles(fi.Directory.FullName, fi.Name.Replace(fi.Extension, "*"));
                    return probes?.FirstOrDefault();
                }
            }

            return string.Empty;
        }

        public static string GetSafeInstallUri(Package p)
        {
            string result = string.Empty;
            if (OSVersionHelper.WindowsVersionHelper.IsWindows10October2018OrGreater)
            {
                var info = p.GetAppInstallerInfo();
                if (info != null)
                {
                    result = info.Uri.ToString();
                }
                else
                {
                    result = "AppInstaller info not found";
                }
            }
            else
            {
                result = "AppInstaller info not available";
            }
            return result;
        }


        [Flags]
        private enum PackageType : uint
        {
            None = 0,
            Main = 1,
            Framework = 2,
            Resource = 4,
            Bundle = 8,
            Xap = 16,
            Other = 32,
            All = Main | Framework | Bundle | Xap | Resource | Other
        }
    }
}