using Discord;
using DiscordTemplateBot.Commands.Extensions;
using DiscordTemplateBot.Console.Extensions;
using DiscordTemplateBot.DiscordBotConfiguration;
using DiscordTemplateBot.DiscordBotConfiguration.Extensions;
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
        var discordLogSeverity = GetDiscordLogSeverity(builder.Configuration);
        services.AddDiscordBotConfiguration(discordLogSeverity);
        services.AddDiscordCommands(discordLogSeverity);
    })
    .Build();

await host.RunAsync();
return;

LogSeverity GetDiscordLogSeverity(IConfiguration configuration)
{
    return configuration.GetSection(DiscordBotConfiguration.SectionName)
        .Get<DiscordBotConfiguration>()!.LogLevel;
}