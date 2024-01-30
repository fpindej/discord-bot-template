# Discord .NET template bot

*Discord .NET template bot* is a project utilizing [Discord.NET](https://discordnet.dev/) library to create a Discord bot using .NET ecosystem.

The entire focus of the project is to create a usable template by anyone who just wants to dive in and start coding commands, without the need to set the entire application.

## Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- Discord bot

> :bulb: Create Your bot on [Discord developer portal](https://discord.com/developers/applications)
- Discord account and server (guild)
    - to test the actual bot


## Features
- running as a service using the [.NET generic host](https://learn.microsoft.com/en-us/dotnet/core/extensions/generic-host)
- [Serilog](https://github.com/serilog/serilog) logging
- [Discord.NET](https://discordnet.dev/) library as the application core


## Getting started

### Running the project

1. clone the repository
2. setup Discord bot token
    - open the `appsettings.json` 
    - locate the `DiscordBotConfiguration` section
    - locate the `Token` field and fill in Your Discord bot token
3. run the project
    - choose the appropriate launch configuration
    - hit the run button in Your IDE or use the command:
    ```bash
    dotnet run
    ```

Your bot should be up and running! :tada:

### Test commands

By default this repository implements two simple [slash commands](https://discordnet.dev/guides/int_basics/application-commands/intro.html).

While Your bot is currently up and running, You can go test these commands to see if everyting works fine and You're good to go coding. :blush:

> :warning: If You're getting an error that the interaction couldn't be completed within 3 seconds, [synchronize Your system clock](https://github.com/discord-net/Discord.Net/issues/2010) on Your computer.

## Contributions

:rocket: Excited to contribute? Fantastic! Here's how You can get involved:

### Create an issue
Found a bug or have a suggestion? Open an issue. Feedback is highly valued!

### Take an issue
Explore the open issues, find one that aligns with Your skills or interests, and tackle it.

### Create a pull request
Want to add a new feature or fix a bug? Fork the repository, create a new branch, make your changes, and submit a pull request. Clean, readable, and efficient code will be appreciated. :pray: