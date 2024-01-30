using Discord;
using Discord.Commands;
using Discord.Interactions;
using Discord.WebSocket;
using DiscordTemplateBot.Commands.Services;
using Microsoft.Extensions.DependencyInjection;

namespace DiscordTemplateBot.Commands.Extensions;

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
        var config = new CommandServiceConfig
        {
            DefaultRunMode = Discord.Commands.RunMode.Async,
            LogLevel = logSeverity
        };
        services.AddSingleton(_ => new CommandService(config));

        return services;
    }
}