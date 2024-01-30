using Discord;
using Discord.WebSocket;
using DiscordTemplateBot.DiscordBotConfiguration.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DiscordTemplateBot.DiscordBotConfiguration.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDiscordBotConfiguration(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddOptions<DiscordBotConfiguration>()
            .BindConfiguration(DiscordBotConfiguration.SectionName)
            .ValidateDataAnnotations()
            .ValidateOnStart();

        services.AddDiscordSocketClient(configuration);
        services.AddHostedService<DiscordBotStartupService>();

        return services;
    }

    private static IServiceCollection AddDiscordSocketClient(this IServiceCollection services,
        IConfiguration configuration)
    {
        var logSeverity = GetDiscordLogSeverity(configuration);
        var config = new DiscordSocketConfig
        {
            GatewayIntents = GatewayIntents.All,
            LogGatewayIntentWarnings = false,
            AlwaysDownloadUsers = true,
            LogLevel = logSeverity
        };
        services.AddSingleton(_ => new DiscordSocketClient(config));

        return services;
    }

    private static LogSeverity GetDiscordLogSeverity(IConfiguration configuration)
    {
        return configuration.GetSection(DiscordBotConfiguration.SectionName)
            .Get<DiscordBotConfiguration>()!.LogLevel;
    }
}