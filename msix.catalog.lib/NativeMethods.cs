using System;
using System.Runtime.InteropServices;
using System.Text;

namespace msix.catalog.lib
{
    internal static class NativeMethods
    {
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern int ClosePackageInfo(PACKAGE_INFO_REFERENCE packageInfoReference);

        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern int OpenPackageInfoByFullName(string packageFullName, uint reserved, ref PACKAGE_INFO_REFERENCE packageInfoReference);

        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern int GetPackageApplicationIds(PACKAGE_INFO_REFERENCE packageInfoReference, ref uint bufferLength, IntPtr buffer, out uint count);

        [DllImport("api-ms-win-appmodel-runtime-l1-1-1.dll", CharSet = CharSet.Unicode)]
        internal static extern int GetStagedPackagePathByFullName([MarshalAs(UnmanagedType.LPWStr)] string packageFullName, [In][Out] ref int pathLength, [Out] StringBuilder path);

        [DllImport("api-ms-win-security-base-l1-1-0.dll", SetLastError = true)]
        internal static extern bool GetTokenInformation(IntPtr tokenHandle, TOKEN_INFORMATION_CLASS tokenInformationClass, IntPtr tokenInformation, uint tokenInformationLength, out uint returnLength);

        [DllImport("api-ms-win-security-sddl-l1-1-0.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        internal static extern bool ConvertSidToStringSid(IntPtr pSID, out IntPtr ptrSid);

        [DllImport("api-ms-win-core-handle-l1-1-0.dll", SetLastError = true)]
        internal static extern bool CloseHandle(IntPtr hHandle);

        [DllImport("api-ms-win-core-processthreads-l1-1-0.dll")]
        internal static extern IntPtr GetCurrentProcess();

        [DllImport("api-ms-win-core-processsecurity-l1-1-0.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool OpenProcessToken(IntPtr processHandle, uint desiredAccess, out IntPtr tokenHandle);
    }
}