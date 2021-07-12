using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.Azure.Management.Media.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Threading.Tasks;

namespace AMSExplorer
{
    /// <summary>
    /// Telemtry using Application Insights
    /// </summary>
    public static class Telemetry
    {
        private static string TelemetryKeyDev = "2c258d64-b2e4-4219-8108-e938f2f4fc02";
        private static string TelemetryKeyProd = "5fb7cbe4-882c-4df8-ad29-df9733248597";
        private static TelemetryClient _telemetry;

        public static bool Enabled { get; set; } = false;

        private static TelemetryClient GetAppInsightsClient()
        {
            var config = new TelemetryConfiguration()
            {
                InstrumentationKey = TelemetryKeyProd,
                TelemetryChannel = new Microsoft.ApplicationInsights.WindowsServer.TelemetryChannel.ServerTelemetryChannel(),
            };
            config.TelemetryChannel.DeveloperMode = Debugger.IsAttached;
#if DEBUG
            config.InstrumentationKey = TelemetryKeyDev;
            config.TelemetryChannel.DeveloperMode = true;
#endif
            TelemetryClient client = new(config);
            client.Context.Component.Version = Assembly.GetEntryAssembly().GetName().Version.ToString();
            client.Context.Session.Id = Guid.NewGuid().ToString();

            // let's anonymize the user id
            //client.Context.User.Id = (Environment.UserName + Environment.MachineName).GetHashCode().ToString();
            client.Context.User.Id = (Environment.UserName + Environment.MachineName).GetDeterministicHashCode().ToString();
            client.Context.Device.OperatingSystem = Environment.OSVersion.ToString();
            client.Context.Cloud.RoleName = "AMSE";

            // let's anonymize role instance
            client.Context.Cloud.RoleInstance = Environment.MachineName.GetDeterministicHashCode().ToString();

            // let's log the version of the Media SDK
            var mediaAssembly = System.Reflection.Assembly.GetAssembly(typeof(MediaService)).GetName();
            client.Context.GlobalProperties.Add(mediaAssembly.Name, mediaAssembly.Version.ToString());

            return client;
        }

        public static void SetUser(string user)
        {
            _telemetry.Context.User.AuthenticatedUserId = user;
        }

        public static void TrackPageView(string pageName)
        {
            if (Enabled)
            {
                _telemetry.TrackPageView(pageName);
            }
        }

        public static void TrackEvent(string key, IDictionary<string, string> properties = null, IDictionary<string, double> metrics = null)
        {
            if (Enabled)
            {
                _telemetry.TrackEvent(key, properties, metrics);
            }
        }

        public static void TrackException(Exception ex)
        {
            if (ex != null && Enabled)
            {
                Dictionary<string, string> dic = new();
                if (ex.InnerException != null)
                {
                    dic.Add("InnerExceptionMessage", Program.GetErrorMessage(ex));
                }
                if (ex is ApiErrorException eApi)
                {
                    dynamic error = JsonConvert.DeserializeObject(eApi.Response.Content);
                    dic.Add("ApiErrorExceptionMessage", (string)error?.error?.message);
                }

                //var telex = new Microsoft.ApplicationInsights.DataContracts.ExceptionTelemetry(ex);
                //_telemetry.TrackException(telex);

                _telemetry.TrackException(ex, dic);
                Flush();
            }
        }

        internal static void Flush()
        {
            _telemetry.Flush();
        }


        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            var ex = e.ExceptionObject as Exception;
            Telemetry.TrackException(ex);
        }

        private static void TaskScheduler_UnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e)
        {
            Telemetry.TrackException(e.Exception);
        }

        private static void CurrentDomain_ProcessExit(object sender, EventArgs e)
        {
            Telemetry.TrackEvent("Application exited");
        }

        public static void StartTelemetry()
        {
            _telemetry = GetAppInsightsClient();
            Enabled = true;
            AppDomain.CurrentDomain.ProcessExit += CurrentDomain_ProcessExit;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            TaskScheduler.UnobservedTaskException += TaskScheduler_UnobservedTaskException;
            Telemetry.TrackEvent("Application started");
        }


        static int GetDeterministicHashCode(this string str)
        {
            unchecked
            {
                int hash1 = (5381 << 16) + 5381;
                int hash2 = hash1;

                for (int i = 0; i < str.Length; i += 2)
                {
                    hash1 = ((hash1 << 5) + hash1) ^ str[i];
                    if (i == str.Length - 1)
                        break;
                    hash2 = ((hash2 << 5) + hash2) ^ str[i + 1];
                }

                return hash1 + (hash2 * 1566083941);
            }
        }
    }
}
