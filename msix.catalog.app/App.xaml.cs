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
        public static Stopwatch Clock = Stopwatch.StartNew();

        public App()
        {
            this.LoadCompleted += App_LoadCompleted;

            DiagnosticsClient.Initialize();
        }

        private void App_LoadCompleted(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {
            Clock.Stop();
            DiagnosticsClient.TrackEvent("StartupPerf", 
                new Dictionary<string, string> { { "DeploymentType", ThisAppVersionInfo.GetDeploymentType()}}, 
                new Dictionary<string, double>{{ "startupTime", Clock.ElapsedMilliseconds}});
        }
    }
}
