using System;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.ServiceProcess;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Web;
using ILogger = NLog.ILogger;

namespace EnterpriseSimpleV2.Host
{
    public class Program
    {
        public static void Main(string[] args)
        {
            GlobalDiagnosticsContext.Set("logDir",
                $"{Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)}/Web/logs");

            var logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();

#if DEBUG
            LogManager.ThrowExceptions = true;
#endif
            try
            {
                StartWebService(args);
            }
            catch (Exception e)
            {
                logger.Error(e);
                throw;
            }
        }

        private static void StartWebService(string[] args)
        {
            if (RunsAsService())
            {
                Environment.CurrentDirectory = BaseDirectory;

                ServiceBase.Run(new ServiceBase[] {new MyServerService()});
            }
            else
            {
                BuildWebHost(args).Run();
                LogManager.Shutdown();
            }
        }

        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();

        private static bool CheckForConsoleWindow()
        {
            return GetConsoleWindow() == IntPtr.Zero;
        }

        private static bool RunsAsService()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows) &&
                Microsoft.Azure.Web.DataProtection.Util.IsAzureEnvironment() == false)
            {
                return CheckForConsoleWindow();
            }

            return false; // never can be a windows service
        }

        private sealed class MyServerService : ServiceBase
        {
            private IWebHost _webHost;
            private readonly ILogger _logger = LogManager.GetCurrentClassLogger();

            protected override void OnStart(string[] args)
            {
                try
                {
                    _webHost = BuildWebHost(args);
                    _webHost.Start();
                }
                catch (Exception e)
                {
                    _logger.Fatal(e);
                    throw;
                }
            }

            protected override void OnStop()
            {
                LogManager.Shutdown();
                _webHost.Dispose();
            }
        }

        private static string BaseDirectory => Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

        private static IWebHost BuildWebHost(string[] args)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("hosting.json", optional: true)
                .AddCommandLine(args).Build();
            return WebHost.CreateDefaultBuilder(args)
                .UseKestrel()
                .UseConfiguration(config)
                .UseStartup<Startup>()
                .ConfigureLogging(logging => { logging.ClearProviders(); })
                .UseNLog()
                .Build();
        }
    }
}