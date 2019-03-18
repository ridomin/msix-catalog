using System;
using System.Collections.Generic;
using System.Text;
using Windows.ApplicationModel;
using System.Runtime.CompilerServices;

namespace msix.catalog.lib
{
    internal class WinRTMethods
    {
        [MethodImpl(MethodImplOptions.NoInlining)]
        internal static Uri GetAppInstallerInfoUri()
        {
            return Package.Current.GetAppInstallerInfo().Uri;
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        internal static Uri GetAppInstallerInfoUri(Package p)
        {
            var aiInfo = p.GetAppInstallerInfo();
            if (aiInfo != null )
            {
                return aiInfo.Uri;
            }
            return null;
        }
    }
}
