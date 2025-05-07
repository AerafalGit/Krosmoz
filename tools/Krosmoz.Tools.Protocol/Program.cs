using Krosmoz.Core.Extensions;
using Krosmoz.Servers.GameServer.Database.Repositories.Datacenter;
using Krosmoz.Tools.Protocol.Generators;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

await Host.CreateDefaultBuilder(args)
    .UseSerilogLogging()
    .ConfigureServices(static services =>
    {
        services
            .AddSingleton<IDatacenterRepository, DatacenterRepository>()
            .AddHostedService<ProtocolGenerator>();
    })
    .RunConsoleAsync();
