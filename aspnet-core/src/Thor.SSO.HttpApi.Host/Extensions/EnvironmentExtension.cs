using Microsoft.Extensions.Hosting;
using Serilog;
using System;

namespace Thor.SSO.Extensions
{
    public static class EnvironmentExtension
    {
        const string TestEnvironment = "Test";
        const string CIEnvironment = "CI";

        public static bool IsDockerEnvironment()
        {
            return Environment.GetEnvironmentVariable("DOTNET_RUNNING_IN_CONTAINER") == "true";
        }

        public static bool IsDevelopmentOverall(this IHostEnvironment env)
        {
            return env.IsDevelopment(); // Add custom environments if you so require
        }

        public static bool IsProductionOverall(this IHostEnvironment env)
        {
            return env.IsProduction(); // Add custom environments if you so require
        }

        // The name staging is the same as the build Configuration.
        // Decided to fallback to 'Test' so we have clear distinction between build Configuration and runtime Environment.
        public static bool IsTest(this IHostEnvironment env)
        {
            return env.IsEnvironment(TestEnvironment);
        }

        public static bool IsCI(this IHostEnvironment env)
        {
            return env.IsEnvironment(CIEnvironment);
        }

        public static void LogConfigurationAndEnvironment(this IHostEnvironment env)
        {
#if DEBUG
            Log.Information($"Current environment (ASPNETCORE_ENVIRONMENT): {env.EnvironmentName}");
            Log.Information($"IsDevelopmentOverall: {env.IsDevelopmentOverall()}");
#elif STAGING
            Log.Information("Staging mode.");
            Log.Information($"IsTest: {env.IsTest()}");
#elif RELEASE
            Log.Information("Release mode.");
#endif
            Log.Information($"IsDockerEnvironment: {IsDockerEnvironment()}");
            Log.Information($"IsProductionOverall: {env.IsProductionOverall()}");
        }
    }
}
