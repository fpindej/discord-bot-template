using Discord.Interactions;

namespace DiscordTemplateBot.Commands.Modules;

public sealed class TestModule : InteractionModuleBase<SocketInteractionContext>
{
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