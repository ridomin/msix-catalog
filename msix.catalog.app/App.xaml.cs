using Microsoft.ApplicationInsights.DataContracts;
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
        public static Microsoft.ApplicationInsights.TelemetryClient TelemetryClient = new Microsoft.ApplicationInsights.TelemetryClient();

        public static Stopwatch Clock = Stopwatch.StartNew();

        public App()
        {
            this.Startup += App_Startup;
            this.Exit += App_Exit;
            this.LoadCompleted += App_LoadCompleted;
            this.DispatcherUnhandledException += App_DispatcherUnhandledException;
        }

        private void App_LoadCompleted(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {
            Clock.Stop();
            var eventTelemetry = new EventTelemetry("StartupPerf");
            eventTelemetry.Metrics.Add("startupTime", Clock.ElapsedMilliseconds);
            eventTelemetry.Properties.Add("DeploymentType", ThisAppVersionInfo.GetDeploymentType());
            TelemetryClient.TrackEvent(eventTelemetry);
        }

        private void App_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            TelemetryClient.TrackException(e.Exception);
        }

        private void App_Exit(object sender, ExitEventArgs e)
        {
            if (TelemetryClient != null)
            {
                TelemetryClient.Flush(); // only for desktop apps
            }
        }

        private void App_Startup(object sender, StartupEventArgs e)
        {
            
            TelemetryClient.InstrumentationKey = "52dd7c90-6256-4d23-bbc5-e3e175c632f4";

            // Set session data:
            TelemetryClient.Context.User.Id = Environment.UserName;
            TelemetryClient.Context.Session.Id = Guid.NewGuid().ToString();
            TelemetryClient.Context.Device.OperatingSystem = Environment.OSVersion.ToString();
            TelemetryClient.Context.Component.Version = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            TelemetryClient.Context.GlobalProperties.Add("DeploymentType", ThisAppVersionInfo.GetDeploymentType());
            // Log a page view:
            TelemetryClient.TrackTrace("App Start");
        }

       
    }
}
