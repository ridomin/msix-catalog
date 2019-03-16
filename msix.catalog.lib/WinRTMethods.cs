using System;
using System.Collections.Generic;
using System.Text;
using Windows.ApplicationModel;

namespace msix.catalog.lib
{
    internal class WinRTMethods
    {
        internal static Uri GetAppInstallerInfoUri()
        {
            return Package.Current.GetAppInstallerInfo().Uri;
        }

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
