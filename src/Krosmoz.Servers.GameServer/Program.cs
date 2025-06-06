using Krosmoz.Core.Extensions;
using Krosmoz.Core.Network.Hosting;
using Krosmoz.Core.Network.Protocol.Dofus;
using Krosmoz.Core.Scheduling;
using Krosmoz.Core.Services;
using Krosmoz.Protocol.Ipc;
using Krosmoz.Servers.GameServer.Commands;
using Krosmoz.Servers.GameServer.Database;
using Krosmoz.Servers.GameServer.Models.Options.Breeds;
using Krosmoz.Servers.GameServer.Models.Options.OptionalFeatures;
using Krosmoz.Servers.GameServer.Models.Options.Servers;
using Krosmoz.Servers.GameServer.Network.Transport;
using Krosmoz.Servers.GameServer.Services.Chat;
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
    .UseNats("krosmoz-nats", IpcJsonSerializerContext.WithCustomConverters)
    .UseCompositeServer(static server => server.ListenFromEnvironment(static connection => connection.UseConnectionHandler<DofusConnectionHandler>()));

builder.Services
    .AddDofusProtocol()
    .AddCommands()
    .AddInitializableServices()
    .Configure<ServerOptions>(builder.Configuration)
    .Configure<OptionalFeaturesOptions>(builder.Configuration)
    .Configure<BreedOptions>(builder.Configuration)
    .AddTransient<IScheduler, Scheduler>()
    .AddSingleton<IDatacenterService, DatacenterService>()
    .AddSingleton<IOptionalFeatureService, OptionalFeatureService>()
    .AddSingleton<IServerService, ServerService>()
    .AddScoped<IChatService, ChatService>();

builder
    .Build()
    .Run();
