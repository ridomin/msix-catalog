using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Xml;
using Windows.ApplicationModel;

namespace msix.catalog.lib
{
    public static class PackageExtensions
    {
        private const int ERROR_SUCCESS = 0;
        private const int ERROR_INSUFFICIENT_BUFFER = 122;

        [SecurityCritical]
        public static string GetStagedPackagePathByFullName(this Package p)
        {
            string packageFullName = p.Id.FullName;
            var size = 0;
            var result = NativeMethods.GetStagedPackagePathByFullName(packageFullName, ref size, null);

            if (result != ERROR_INSUFFICIENT_BUFFER)
            {
                return null;
            }

            var sb = new StringBuilder(size);
            result = NativeMethods.GetStagedPackagePathByFullName(packageFullName, ref size, sb);
            if (result != ERROR_SUCCESS)
            {
                return null;
            }

            return sb.ToString();
        }

        public static IEnumerable<string> GetAllAppUserModelId(this Package package)
        {
            var listAppIds = new List<string>();
            PACKAGE_INFO_REFERENCE pir = new PACKAGE_INFO_REFERENCE();

            int rc = NativeMethods.OpenPackageInfoByFullName(package.Id.FullName, 0, ref pir);
            if (rc == 0)
            {
                IntPtr buffer = IntPtr.Zero;

                uint count;
                uint length = 0;

                try
                {
                    rc = NativeMethods.GetPackageApplicationIds(pir, ref length, IntPtr.Zero, out count);
                    if (rc == ERROR_INSUFFICIENT_BUFFER)
                    {
                        buffer = Marshal.AllocHGlobal((int)length);

                        if (buffer != IntPtr.Zero)
                        {
                            rc = NativeMethods.GetPackageApplicationIds(pir, ref length, buffer, out count);

                            IntPtr pStr = buffer;
                            for (uint i = 0; i < count; ++i)
                            {
                                string sAppId = Marshal.PtrToStringUni(Marshal.ReadIntPtr(pStr));
                                listAppIds.Add(sAppId);

                                pStr += Marshal.SizeOf(typeof(IntPtr));
                            }
                        }
                    }
                }
                catch { }
                finally
                {
                    if (buffer != IntPtr.Zero)
                    {
                        Marshal.FreeHGlobal(buffer);
                    }
                    NativeMethods.ClosePackageInfo(pir);
                }
            }

            return listAppIds;
        }

        public static string GetFirstAppUserModelId(this Package package)
        {
            return GetFirstAppUserModelId(package, package.GetStagedPackagePathByFullName());
        }

        public static string GetFirstAppUserModelId(this Package package, string packageLocation)
        {
            PACKAGE_INFO_REFERENCE pir = new PACKAGE_INFO_REFERENCE();

            int rc = NativeMethods.OpenPackageInfoByFullName(package.Id.FullName, 0, ref pir);
            if (rc == 0)
            {
                IntPtr buffer = IntPtr.Zero;

                uint count;
                uint length = 0;

                try
                {
                    rc = NativeMethods.GetPackageApplicationIds(pir, ref length, IntPtr.Zero, out count);
                    if (rc == ERROR_INSUFFICIENT_BUFFER)
                    {
                        buffer = Marshal.AllocHGlobal((int)length);

                        if (buffer != IntPtr.Zero)
                        {
                            rc = NativeMethods.GetPackageApplicationIds(pir, ref length, buffer, out count);
                            if (rc == 0 && count > 0)
                            {
                                return Marshal.PtrToStringUni(Marshal.ReadIntPtr(buffer));
                            }
                        }
                    }
                }
                catch { }
                finally
                {
                    if (buffer != IntPtr.Zero)
                    {
                        Marshal.FreeHGlobal(buffer);
                    }
                    NativeMethods.ClosePackageInfo(pir);
                }
            }

            return GetFirstAppModelId(packageLocation, package.Id.FamilyName);
        }

        private static string GetFirstAppModelId(string installedPackageLocation, string familyName)
        {
            if (!string.IsNullOrWhiteSpace(installedPackageLocation))
            {
                var appxManifestPath = Path.Combine(installedPackageLocation, "AppXManifest.xml");

                if (File.Exists(appxManifestPath))
                {
                    using (var fs = new FileStream(appxManifestPath, FileMode.Open, FileAccess.Read))
                    {
                        using (var reader = XmlReader.Create(fs))
                        {
                            if (reader.ReadToDescendant("Application"))
                            {
                                var applicationId = reader.GetAttribute("Id");
                                if (!string.IsNullOrWhiteSpace(applicationId))
                                {
                                    return $"{familyName}!{applicationId}";
                                }
                            }
                        }
                    }
                }
            }

            return string.Empty;
        }
    }
}
