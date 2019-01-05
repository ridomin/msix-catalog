using System;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;
using Windows.ApplicationModel;
using System.IO;
using System.Xml.Linq;
using System.Threading.Tasks;

namespace msix.catalog.lib
{
    public class PackageRepository
    {        
        public static async  Task<IList<PackageInfo>> LoadAllInstalledApps()
        {
            Stopwatch clock = Stopwatch.StartNew();
            var mgr = new Windows.Management.Deployment.PackageManager();
            IEnumerable<Package> allPackages;

            return await Task.Run(() => 
            {
                allPackages =  mgr.FindPackagesForUser(string.Empty);

                var q = from p in allPackages
                        select new PackageInfo
                        {
                            Id = p.Id.Name,
                            LogoUri = GetSafeLogo(p),
                            PackageName = p.DisplayName,
                            PFN = p.Id.FamilyName + "!App",
                            Author = p.Id.Publisher,
                            SignatureKind = p.SignatureKind.ToString(),
                            Architecture = p.Id.Architecture.ToString(),
                            //AppInstallerUri = p.GetAppInstallerInfo()?.Uri.ToString() ,
                            IsFramework = p.IsFramework,
                            InstalledDate = p.InstalledDate.UtcDateTime,
                            InstallLocation = GetSafeInstalledLocation(p),
                            Version = $"{p.Id.Version.Major}.{p.Id.Version.Minor}.{p.Id.Version.Build}.{p.Id.Version.Revision}"
                        };

                return q.ToList();
            });
        }

        private static string GetSafeInstalledLocation(Package p)
        {
            var result = string.Empty;
            try
            {
                result = p.InstalledLocation.Path;
            }
            catch (Exception ex)
            {
                //App.TelemetryClient.TrackTrace("Handling exception for app: " + p.Id.FullName);
                //App.TelemetryClient.TrackException(ex);
                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }
            return result;
        }

        private static string GetSafeLogo(Package p)
        {
            var loc = GetSafeInstalledLocation(p);
            var manifestPath = Path.Combine(loc, "AppxManifest.xml");
            if (!File.Exists(manifestPath))
            {
                string msg = $"Package {p.Id.FullName} location is not set. Unable to find the manifest.";
                System.Diagnostics.Debug.WriteLine(msg);
                //App.TelemetryClient.TrackTrace(msg);
                return "";
            }
            else
            {
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
                    else
                    {
                        if (fi.Directory.Exists)
                        {
                            var probes = Directory.EnumerateFiles(fi.Directory.FullName,
                                                        fi.Name.Replace(fi.Extension, "*"));
                            foreach (var f in probes)
                            {
                                return f;
                            }
                        }
                    }
                }
            }
            return "";
        }
    }
}
