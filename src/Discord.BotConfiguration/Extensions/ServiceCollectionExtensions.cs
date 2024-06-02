using Discord.BotConfiguration.Services;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Discord.BotConfiguration.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDiscordBotConfiguration(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddOptions<BotConfiguration>()
            .BindConfiguration(BotConfiguration.SectionName)
            .ValidateDataAnnotations()
            .ValidateOnStart();

        services.AddDiscordSocketClient(configuration);
        services.AddHostedService<DiscordBotStartupService>();

        return services;
    }

    private static IServiceCollection AddDiscordSocketClient(this IServiceCollection services,
        IConfiguration configuration)
    {
        var logSeverity = LoggerHelper.GetDiscordLogSeverity(configuration);
        var config = GetDiscordSocketConfig(logSeverity);
        services.AddSingleton(_ => new DiscordSocketClient(config));

        return services;
    }

    private static DiscordSocketConfig GetDiscordSocketConfig(LogSeverity logSeverity)
    {
        return new DiscordSocketConfig
        {
            // Make sure you set your intents according to your bot's needs.
            // More about intents: https://discord.com/developers/docs/topics/gateway#gateway-intents
            GatewayIntents = GatewayIntents.All,
            LogGatewayIntentWarnings = false,
            UseInteractionSnowflakeDate = false,
            AlwaysDownloadUsers = true,
            LogLevel = logSeverity
        };
    }
}