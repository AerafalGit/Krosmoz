using Krosmoz.Core.Extensions;
using Krosmoz.Core.Network.Hosting;
using Krosmoz.Core.Network.Protocol.Dofus;
using Krosmoz.Core.Scheduling;
using Krosmoz.Servers.AuthServer.Database;
using Krosmoz.Servers.AuthServer.Network.Transport;
using Microsoft.AspNetCore.Connections;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = Host.CreateApplicationBuilder(args);

builder
    .AddServiceDefaults()
    .UseNpgsqlDbContext<AuthDbContext>("krosmoz-auth-db")
    .UseNats("krosmoz-nats")
    .UseCompositeServer(static server => server.ListenFromEnvironment(static connection => connection.UseConnectionHandler<DofusConnectionHandler>()));

builder.Services
    .AddDofusProtocol()
    .AddTransient<IScheduler, Scheduler>();

builder
    .Build()
    .Run();
