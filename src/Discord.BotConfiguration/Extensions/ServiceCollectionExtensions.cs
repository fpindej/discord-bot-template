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
        var config = GetDiscordSocketConfig(logSeverity);
        services.AddSingleton(_ => new DiscordSocketClient(config));

        return services;
    }
    
    private static DiscordSocketConfig GetDiscordSocketConfig(LogSeverity logSeverity)
    {
        return new DiscordSocketConfig
        {
            GatewayIntents = GatewayIntents.AllUnprivileged,
            LogGatewayIntentWarnings = false,
            UseInteractionSnowflakeDate = false,
            AlwaysDownloadUsers = true,
            LogLevel = logSeverity
        };
    }
}