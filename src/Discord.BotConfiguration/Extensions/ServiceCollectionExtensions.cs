using Discord.BotConfiguration.Services;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;

namespace Discord.BotConfiguration.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDiscordBotConfiguration(this IServiceCollection services,
        LogSeverity logSeverity)
    {
        services.AddOptions<BotConfiguration>()
            .BindConfiguration(BotConfiguration.SectionName)
            .ValidateDataAnnotations()
            .ValidateOnStart();

        services.AddDiscordSocketClient(logSeverity);
        services.AddHostedService<DiscordBotStartupService>();

        return services;
    }

    private static IServiceCollection AddDiscordSocketClient(this IServiceCollection services,
        LogSeverity logSeverity)
    {
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
}