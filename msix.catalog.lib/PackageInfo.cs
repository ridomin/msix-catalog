using System;
using System.Collections.Generic;
using System.Text;

namespace msix.catalog.lib
{
    public class PackageInfo
    {
        public string LogoUri { get; set; }
        public string Id { get; set; }
        public string Version { get; set; }
        public string Author { get; set; }
        public string PackageName { get; set; }
        public string Architecture { get; set; }
        public string AppInstallerUri { get; set; }
        public string SignatureKind { get; set; }
        public string PFN { get; set; }
        public bool IsFramework { get; set; }
        public DateTime? InstalledDate { get; set; }
        public string InstallLocation { get; set; }
    }
}
