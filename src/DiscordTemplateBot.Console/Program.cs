using DiscordTemplateBot.Console.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

using var host = Host.CreateDefaultBuilder(args)
    .ConfigureSerilog()
    .ConfigureAppConfiguration(config =>
    {
        var environmentName = Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT");

        config.AddJsonFile("appsettings.json", false, true);
        config.AddJsonFile($"appsettings.{environmentName}.json", true, true);
    })
    .Build();

await host.RunAsync();