using System.ComponentModel.DataAnnotations;
using Discord;

namespace DiscordTemplateBot.DiscordBotConfiguration;

public sealed class DiscordBotConfiguration
{
    public const string SectionName = "DiscordBotConfiguration";

    [Required] 
    public string Token { get; init; } = null!;

    public LogSeverity LogLevel { get; init; } = LogSeverity.Info;
}