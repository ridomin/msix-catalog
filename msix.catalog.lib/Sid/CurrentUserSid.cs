using System;
using System.Runtime.InteropServices;
using System.Security;

namespace msix.catalog.lib
{
    public static class CurrentUserSid
    {
        private const int TOKEN_QUERY = 0x00000008;

        [SecurityCritical]
        public static string Get()
        {
            IntPtr processHandle = NativeMethods.GetCurrentProcess();
            IntPtr stringSid = IntPtr.Zero;
            IntPtr tokenHandle = IntPtr.Zero;
            IntPtr tokenInformation = IntPtr.Zero;

            try
            {
                if (!NativeMethods.OpenProcessToken(processHandle, TOKEN_QUERY, out tokenHandle))
                {
                    return null;
                }

                uint tokenLength = 0;
                NativeMethods.GetTokenInformation(tokenHandle, TOKEN_INFORMATION_CLASS.TokenUser, IntPtr.Zero, tokenLength, out tokenLength);

                tokenInformation = Marshal.AllocHGlobal((int)tokenLength);

                if (!NativeMethods.GetTokenInformation(tokenHandle, TOKEN_INFORMATION_CLASS.TokenUser, tokenInformation, tokenLength, out tokenLength))
                {
                    return null;
                }

                var tokenUser = (TOKEN_USER)Marshal.PtrToStructure(tokenInformation, typeof(TOKEN_USER));

                if (!NativeMethods.ConvertSidToStringSid(tokenUser.User.Sid, out stringSid))
                {
                    return null;
                }

                return Marshal.PtrToStringUni(stringSid);
            }
            finally
            {
                if (stringSid != IntPtr.Zero)
                {
                    Marshal.FreeHGlobal(stringSid);
                }

                if (tokenHandle != IntPtr.Zero)
                {
                    NativeMethods.CloseHandle(tokenHandle);
                }

                if (tokenInformation != IntPtr.Zero)
                {
                    Marshal.FreeHGlobal(tokenInformation);
                }
            }
        }
    }
}