using Krosmoz.Core.Extensions;
using Krosmoz.Core.Network.Hosting;
using Krosmoz.Core.Scheduling;
using Krosmoz.Servers.GameServer.Database;
using Krosmoz.Servers.GameServer.Network.Transport;
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
    .AddTransient<IScheduler, Scheduler>();

builder
    .Build()
    .Run();
