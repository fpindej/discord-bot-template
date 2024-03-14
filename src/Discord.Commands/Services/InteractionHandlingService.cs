using System.Reflection;
using Discord.Interactions;
using Discord.WebSocket;
using Logging;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Discord.Commands.Services;

public sealed class InteractionHandlingService : IHostedService
{
    private readonly DiscordSocketClient _client;
    private readonly InteractionService _commands;
    private readonly IServiceProvider _services;
    private readonly ILogger<InteractionHandlingService> _logger;

    public InteractionHandlingService(IServiceProvider services, DiscordSocketClient client,
        InteractionService commands,
        ILogger<InteractionHandlingService> logger)
    {
        _services = services;
        _client = client ?? throw new ArgumentNullException(nameof(client));
        _commands = commands ?? throw new ArgumentNullException(nameof(commands));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));

        _commands.Log += LogHelper.LogAsync;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        _client.Ready += () => _commands.RegisterCommandsGloballyAsync();
        _client.InteractionCreated += OnInteractionAsync;

        _logger.LogInformation("Registering commands...");
        await _commands.AddModulesAsync(Assembly.GetExecutingAssembly(), _services);

        _commands.InteractionExecuted += InteractionExecutedAsync;
    }


    public Task StopAsync(CancellationToken cancellationToken)
    {
        _commands.Dispose();
        return Task.CompletedTask;
    }

    private async Task OnInteractionAsync(SocketInteraction interaction)
    {
        try
        {
            var context = new SocketInteractionContext(_client, interaction);
            var result = await _commands.ExecuteCommandAsync(context, _services);

            if (result.IsSuccess is false)
            {
                _logger.LogError("Error handling interaction: {ErrorMessage}", result.ToString());
                await context.Channel.SendMessageAsync(result.ToString());
            }
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error handling interaction.");

            if (interaction.Type is InteractionType.ApplicationCommand)
                await interaction.GetOriginalResponseAsync().ContinueWith(async msg => await msg.Result.DeleteAsync());

            throw;
        }
    }

    private Task InteractionExecutedAsync(ICommandInfo commandInfo, IInteractionContext interactionContext,
        Interactions.IResult result)
    {
        _logger.LogInformation("Interaction command executed: {CommandName}. Server: {Server}. User: {User}",
            commandInfo.Name, interactionContext.Guild?.Name, interactionContext.User?.Username);
        return Task.CompletedTask;
    }
}