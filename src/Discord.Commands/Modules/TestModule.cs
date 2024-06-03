using Discord.Interactions;
using Microsoft.Extensions.Logging;

namespace Discord.Commands.Modules;

public sealed class TestModule : InteractionModuleBase<SocketInteractionContext>
{
    private readonly ILogger<TestModule> _logger;

    public TestModule(ILogger<TestModule> logger)
    {
        _logger = logger;
    }

    [SlashCommand("test", "Test command")]
    public async Task Test()
    {
        await RespondAsync("Test command");
    }

    [SlashCommand("say", "Say something, don't be shy!")]
    public async Task Say(string input)
    {
        await RespondAsync($"You said **{input}**");
    }
}