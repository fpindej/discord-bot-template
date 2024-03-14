using Discord.Commands.Extensions;
using ConsoleApp.Extensions;
using Discord.BotConfiguration.Extensions;
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
    .ConfigureServices((builder, services) =>
    {
        // The sequence of these calls is crucial due to assembly scanning, and they should be placed at the end.
        services.AddDiscordBotConfiguration(builder.Configuration);
        services.AddDiscordCommands(builder.Configuration);
    })
    .Build();

await host.RunAsync();