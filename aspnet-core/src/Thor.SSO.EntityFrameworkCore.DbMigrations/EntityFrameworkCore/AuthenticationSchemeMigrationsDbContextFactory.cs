using Aguacongas.AspNetCore.Authentication.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace Thor.SSO.EntityFrameworkCore
{
    /* This class is needed for EF Core console commands
     * (like Add-Migration and Update-Database commands) */
    public class AuthenticationSchemeMigrationsDbContextFactory : IDesignTimeDbContextFactory<SchemeDbContext>
    {
        public SchemeDbContext CreateDbContext(string[] args)
        {
            //SSOEfCoreEntityExtensionMappings.Configure();

            var configuration = BuildConfiguration();

            var builder = new DbContextOptionsBuilder<SchemeDbContext>()
                .UseSqlServer(configuration.GetConnectionString("Default"), o =>
                {
                    o.MigrationsAssembly(GetType().Assembly.FullName);
                });

            return new SchemeDbContext(builder.Options);
        }

        private static IConfigurationRoot BuildConfiguration()
        {
            Console.WriteLine("Running context migrator for Dynamic Authentication Scheme db context");
            var builder = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../Thor.SSO.DbMigrator/"))
                .AddJsonFile("appsettings.json", optional: false);

            return builder.Build();
        }
    }
}
