using System.ComponentModel.DataAnnotations;

namespace Discord.BotConfiguration;

public sealed class BotConfiguration
{
    public const string SectionName = "DiscordBotConfiguration";

    [Required]
    public required string Token { get; init; }

    public LogSeverity LogLevel { get; init; } = LogSeverity.Info;
}