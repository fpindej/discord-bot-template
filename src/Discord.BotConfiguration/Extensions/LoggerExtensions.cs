using Microsoft.Extensions.Configuration;

namespace Discord.BotConfiguration.Extensions;

public static class LoggerExtensions
{
    public static LogSeverity GetDiscordLogSeverity(IConfiguration configuration)
    {
        return configuration.GetSection(BotConfiguration.SectionName)
            .Get<BotConfiguration>()!.LogLevel;
    }
}