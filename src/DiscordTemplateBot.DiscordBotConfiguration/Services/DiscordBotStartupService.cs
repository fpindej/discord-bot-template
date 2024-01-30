using Discord;
using Discord.WebSocket;
using DiscordTemplateBot.Logging;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace DiscordTemplateBot.DiscordBotConfiguration.Services;

public sealed class DiscordBotStartupService : IHostedService
{
    private readonly DiscordSocketClient _client;
    private readonly ILogger<DiscordBotStartupService> _logger;
    private readonly string _clientToken;

    public DiscordBotStartupService(DiscordSocketClient client, ILogger<DiscordBotStartupService> logger,
        IOptions<DiscordBotConfiguration> discordBotConfiguration)
    {
        _client = client ?? throw new ArgumentNullException(nameof(client));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _clientToken = discordBotConfiguration.Value.Token ??
                       throw new ArgumentNullException(nameof(discordBotConfiguration.Value.Token));

        _client.Log += LogHelper.LogAsync;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogDebug("Establishing connection to Discord...");
        await _client.LoginAsync(TokenType.Bot, _clientToken);
        await _client.StartAsync();
        _logger.LogDebug("Connection established.");
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogDebug("Disconnecting from Discord...");
        await _client.LogoutAsync();
        await _client.StopAsync();
        _logger.LogDebug("Disconnected.");
    }
}