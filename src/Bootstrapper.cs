using Azure.Identity;
using ConsoleAppNet5.Configuration;
using ConsoleAppNet5.Services;
using Microsoft.Azure.KeyVault;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.AzureKeyVault;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.IO;

namespace ConsoleAppNet5
{
    public static class Bootstrapper
    {
        public static IHostBuilder AddLogging(this IHostBuilder host)
        {
            host.ConfigureLogging(logging =>
            {
                logging.ClearProviders();
                logging.AddConsole();
            });
            return host;
        }

        public static IHostBuilder AddConfiguration(this IHostBuilder host)
        {
            // configure this in:
            // 1) Project >  Properties > Debug > Environment variables > Name: DOTNET_ENVIRONMENT, Value: Development
            // 2) dotnet ConsoleAppNet5.dll --environment=Development
            // 3) launchSettings.json
            // context.HostingEnvironment.IsDevelopment() requires an environment called "Development"

            string environmentName = Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT");

            host.ConfigureAppConfiguration((_, configuration) =>
            {
                configuration.Sources.Clear();

                configuration
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: false)
                    .AddJsonFile($"appsettings.{environmentName}.json", optional: true, reloadOnChange: true)
                    .AddUserSecrets<Program>(true); // remember to create a usersecret on your computer
            });
            return host;
        }

        public static IHostBuilder AddServices(this IHostBuilder host)
        {
            host.ConfigureServices((hostingContext, services) =>
            {
                // the hosted service (a singleton) that wrap the console logic that allows DI
                services.AddHostedService<ConsoleHostedService>();
             
                // transient services with your business logic
                services.AddTransient<IRoverService, RoverService>();

            });

            return host;
        }
    }
}
