using Krosmoz.Core.Extensions;
using Krosmoz.Core.Network.Hosting;
using Krosmoz.Core.Scheduling;
using Krosmoz.Servers.GameServer.Database;
using Krosmoz.Servers.GameServer.Models.Options.Breeds;
using Krosmoz.Servers.GameServer.Models.Options.OptionalFeatures;
using Krosmoz.Servers.GameServer.Models.Options.Servers;
using Krosmoz.Servers.GameServer.Network.Transport;
using Krosmoz.Servers.GameServer.Services.Commands;
using Krosmoz.Servers.GameServer.Services.Datacenter;
using Krosmoz.Servers.GameServer.Services.OptionalFeatures;
using Krosmoz.Servers.GameServer.Services.Servers;
using Microsoft.AspNetCore.Connections;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = Host.CreateApplicationBuilder(args);

builder
    .AddServiceDefaults()
    .UseNpgsqlDbContext<GameDbContext>("krosmoz-game-db")
    .UseNats("krosmoz-nats")
    .UseCompositeServer(static server => server.ListenFromEnvironment(static connection => connection.UseConnectionHandler<DofusConnectionHandler>()));

builder.Services
    .Configure<ServerOptions>(builder.Configuration)
    .Configure<OptionalFeaturesOptions>(builder.Configuration)
    .Configure<BreedOptions>(builder.Configuration)
    .AddTransient<IScheduler, Scheduler>()
    .AddSingleton<IDatacenterService, DatacenterService>()
    .AddSingleton<IOptionalFeatureService, OptionalFeatureService>()
    .AddSingleton<IServerService, ServerService>()
    .AddSingleton<ICommandService, CommandService>();

builder
    .Build()
    .Run();
