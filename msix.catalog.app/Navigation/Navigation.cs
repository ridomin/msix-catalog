using msix.catalog.app.Telemetry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace msix.catalog.app.Navigation
{
    public static class Navigation
    {
        private static Frame _frame;

        public static Frame Frame
        {
            get { return _frame; }
            set { _frame = value; }
        }

        public static bool Navigate(Uri sourcePageUri, object extraData = null)
        {
            DiagnosticsClient.TrackPageView(sourcePageUri.ToString());

            if (_frame.CurrentSource != sourcePageUri)
            {
                return _frame.Navigate(sourcePageUri, extraData);
            }
            return true;
        }

        public static bool Navigate(object content)
        {
            if (_frame.NavigationService.Content != content)
            {
                return _frame.Navigate(content);
            }
            return true;
        }

        public static void GoBack()
        {
            if (_frame.CanGoBack)
            {
                DiagnosticsClient.TrackEvent("GoBack");
               _frame.GoBack();
            }
        }
    }
}
