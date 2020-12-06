using App.Metrics;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;

namespace Thor.SSO.Extensions
{
    public static class AppMetricsServiceExtension
    {
        public static void AddAppMetricsInfluxDbMetrics(
            this IServiceCollection services, IConfiguration config)
        {
            services
                .AddMetrics(options =>
                {
                    options.Configuration.Configure(config =>
                    {
                        config.AddAppTag("ThorSSO");
                        config.Enabled = true;
                        config.ReportingEnabled = true;
                    });
                })
                .AddMetricsReportingHostedService()
                .AddMetricsTrackingMiddleware(options =>
                    options.IgnoredHttpStatusCodes = new List<int> { 404 }
                )
                .AddAppMetricsHealthPublishing()
                .AddMetricsEndpoints()
                .AddHealthChecks();

            services.AddControllers().AddMetrics();
        }

        public static void UseAppMetricsMiddleware(
            this IApplicationBuilder app)
        {
            app.UseMetricsAllMiddleware();
            app.UseMetricsAllEndpoints();
        }
    }
}
