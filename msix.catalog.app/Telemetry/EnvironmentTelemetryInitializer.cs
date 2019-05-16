using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.Extensibility;
using msix.catalog.lib;

namespace msix.catalog.app.Telemetry
{
    internal class EnvironmentTelemetryInitializer : ITelemetryInitializer
    {
        public void Initialize(ITelemetry telemetry)
        {
            telemetry.Context.GlobalProperties["Environment"] = ThisAppVersionInfo.GetDeploymentType() ?? "Local";

            // Always default to Local if we're in the debugger
            if (Debugger.IsAttached)
            {
                telemetry.Context.GlobalProperties["Environment"] = "Local";
            }       
        }
    }
}
