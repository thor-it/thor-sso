using System;

namespace Thor.SSO.Extensions
{
    public static class EnvironmentExtensions
    {
        public static bool IsDockerEnvironment()
        {
            return Environment.GetEnvironmentVariable("DOTNET_RUNNING_IN_CONTAINER") == "true";
        }
    }
}
