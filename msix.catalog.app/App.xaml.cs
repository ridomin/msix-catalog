using Humanizer;
using Microsoft.ApplicationInsights.DataContracts;
using msix.catalog.app.Telemetry;
using msix.catalog.app.ViewModels;
using msix.catalog.lib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;

namespace msix.catalog.app
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        Stopwatch clock;
        MainWindow mainWindow;

        public App()
        {
            clock = Stopwatch.StartNew();
            this.LoadCompleted += App_LoadCompleted;
            DiagnosticsClient.Initialize();
            mainWindow = new MainWindow();
            mainWindow.Show();
            mainWindow.Activate();
        }

        private void App_LoadCompleted(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {
            clock.Stop();
            var elapsed = clock.Elapsed;
            mainWindow.statusBarTimeElapsed.Content = elapsed.Humanize(2, false);
            DiagnosticsClient.TrackEvent("StartupPerf", 
                new Dictionary<string, string> { { "DeploymentType", ThisAppVersionInfo.GetDeploymentType()}, { "NetFlavor", ThisAppVersionInfo.GetDotNetInfo()} }, 
                new Dictionary<string, double>{{ "startupTime", elapsed.TotalMilliseconds}});
        }
    }
}
