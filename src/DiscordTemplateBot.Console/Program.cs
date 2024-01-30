using Microsoft.Extensions.Hosting;

using var host = Host.CreateDefaultBuilder(args)
    .Build();

await host.RunAsync();