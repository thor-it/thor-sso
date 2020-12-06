using System;
using App.Metrics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.Grafana.Loki;
using Thor.SSO.Extensions;

namespace Thor.SSO
{
    public class Program
    {
        private static IHostEnvironment _env;
        private static IConfiguration _appConfig;

        public static int Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
#if DEBUG
                .MinimumLevel.Debug()
#else
                .MinimumLevel.Information()
#endif
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                .MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Warning)
                .Enrich.FromLogContext()
                .If(EnvironmentExtension.IsDockerEnvironment(),
                    a => a.WriteTo.GrafanaLoki("http://loki:3100"))
                .If(!EnvironmentExtension.IsDockerEnvironment(),
                    a => a.WriteTo.GrafanaLoki("http://localhost:3100"))
#if DEBUG
                .WriteTo.Async(c => c.Console())
#endif
                .CreateLogger();

            try
            {
                Log.Information("Starting Thor.SSO.HttpApi.Host.");
                CreateHostBuilder(args).Build().Run();
                return 0;
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly!");
                return 1;
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        internal static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostContext, config) =>
                {
                    _env = hostContext.HostingEnvironment;
                    Log.Information($"=== Running Backend in {_env.EnvironmentName} Environment setting. ===");
                    _env.LogConfigurationAndEnvironment();

                    _appConfig = config
                        .SetBasePath(_env.ContentRootPath)
                        .AddJsonFile("appsettings.json", optional: false)
                        .AddJsonFile($"appsettings.{_env.EnvironmentName}.json", optional: false)
                        .AddEnvironmentVariables()
                        .AddCommandLine(args).Build();
                    Log.Information($"=== Found and loaded 'appsettings.{_env.EnvironmentName}.json'\n");
                })
                .ConfigureMetricsWithDefaults(builder =>
                {
                    builder.Report
                        .If(!EnvironmentExtension.IsDockerEnvironment(), a =>
                            a.ToInfluxDb("http://localhost:8086", "thorsso_metrics", TimeSpan.FromSeconds(5)))
                        .If(EnvironmentExtension.IsDockerEnvironment(), a =>
                                a.ToInfluxDb("http://influxdb:8086", "thorsso_metrics", TimeSpan.FromSeconds(5)));
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                .UseAutofac()
                .UseSerilog();
    }
}
