using System.ComponentModel.DataAnnotations;

namespace Discord.BotConfiguration;

public sealed class BotConfiguration
{
    public const string SectionName = "DiscordBotConfiguration";

    [Required]
    public string Token { get; init; } = null!;

    public LogSeverity LogLevel { get; init; } = LogSeverity.Info;
}