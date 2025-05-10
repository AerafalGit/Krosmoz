using Krosmoz.Core.Extensions;
using Krosmoz.Servers.AuthServer.Database;
using Krosmoz.Servers.GameServer.Database.Repositories.Datacenter;
using Krosmoz.Tools.Database.Base;
using Krosmoz.Tools.Database.Hosting;
using Krosmoz.Tools.Database.Servers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

await Host.CreateDefaultBuilder(args)
    .UseSerilogLogging()
    .ConfigureServices(static (context, services) =>
    {
        services
            .AddDbContext<AuthDbContext>(context.Configuration.GetConnectionString("Auth"))
            .AddSingleton<IDatacenterRepository, DatacenterRepository>()
            .AddSingleton<Synchronizer, ServerSynchronizer>()
            .AddHostedService<SynchronizerHostedService>();
    })
    .RunConsoleAsync();
