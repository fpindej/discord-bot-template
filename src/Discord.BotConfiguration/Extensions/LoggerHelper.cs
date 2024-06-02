using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Events;

namespace Discord.BotConfiguration.Extensions;

public static class LoggerHelper
{
    public static LogSeverity GetDiscordLogSeverity(IConfiguration configuration)
    {
        return configuration.GetSection(BotConfiguration.SectionName)
            .Get<BotConfiguration>()!.LogLevel;
    }

    /// <summary>
    ///     Maps Discord.NET's LogSeverity to Serilog's LogEventLevel
    /// </summary>
    /// <param name="message"></param>
    /// <returns></returns>
    public static Task LogAsync(LogMessage message)
    {
        var severity = message.Severity switch
        {
            LogSeverity.Critical => LogEventLevel.Fatal,
            LogSeverity.Error => LogEventLevel.Error,
            LogSeverity.Warning => LogEventLevel.Warning,
            LogSeverity.Info => LogEventLevel.Information,
            LogSeverity.Verbose => LogEventLevel.Verbose,
            LogSeverity.Debug => LogEventLevel.Debug,
            _ => LogEventLevel.Information
        };
        Log.Write(severity, message.Exception, "[{Source}] {Message}", message.Source, message.Message);
        return Task.CompletedTask;
    }
}