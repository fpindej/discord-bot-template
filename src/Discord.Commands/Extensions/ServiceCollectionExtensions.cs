using Discord.Commands.Services;
using Discord.Interactions;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;

namespace Discord.Commands.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDiscordCommands(this IServiceCollection services, LogSeverity logSeverity)
    {
        services.AddDiscordInteractionService();
        services.AddDiscordCommandService(logSeverity);
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
        LogSeverity logSeverity)
    {
        var config = GetCommandServiceConfig(logSeverity);
        services.AddSingleton(_ => new CommandService(config));

        return services;
    }

    private static CommandServiceConfig GetCommandServiceConfig(LogSeverity logSeverity)
    {
        return new CommandServiceConfig
        {
            DefaultRunMode = RunMode.Async,
            LogLevel = logSeverity
        };
    }
}