using Discord.BotConfiguration.Extensions;
using Discord.Commands.Services;
using Discord.Interactions;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Discord.Commands.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDiscordCommands(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDiscordInteractionService();
        services.AddDiscordCommandService(configuration);
        services.AddHostedService<InteractionHandlingService>();

        return services;
    }

    private static IServiceCollection AddDiscordInteractionService(this IServiceCollection services)
    {
        services.AddSingleton(opt =>
        {
            var client = opt.GetRequiredService<DiscordSocketClient>();
            return new InteractionService(client);
        });

        return services;
    }

    private static IServiceCollection AddDiscordCommandService(this IServiceCollection services,
        IConfiguration configuration)
    {
        var config = GetCommandServiceConfig(configuration);
        services.AddSingleton(_ => new CommandService(config));

        return services;
    }

    private static CommandServiceConfig GetCommandServiceConfig(IConfiguration configuration)
    {
        var logSeverity = LoggerHelper.GetDiscordLogSeverity(configuration);

        return new CommandServiceConfig
        {
            DefaultRunMode = RunMode.Async,
            LogLevel = logSeverity
        };
    }
}