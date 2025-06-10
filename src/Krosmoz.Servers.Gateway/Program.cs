using System.Net.Sockets;
using Krosmoz.Servers.Gateway.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Projects;

var builder = DistributedApplication.CreateBuilder(args);

var forceMigrations = builder.Configuration.GetValue<bool>("Database:ForceMigrations");

var forceSeeding = builder.Configuration.GetValue<bool>("Database:ForceSeeding");

var npgsql = builder
    .AddPostgres("krosmoz-postgres")
    .WithDataVolume("krosmoz-postgres-data")
    .WithHostPort(43999)
    .WithLifetime(ContainerLifetime.Persistent);

var authDb = npgsql.AddDatabase("krosmoz-auth-db");

var gameDb = npgsql.AddDatabase("krosmoz-game-db");

var nats = builder
    .AddNats("krosmoz-nats")
    .WithDataVolume("krosmoz-nats-data")
    .WithLifetime(ContainerLifetime.Persistent);

var auth = builder
    .AddProject<Krosmoz_Servers_AuthServer>("krosmoz-auth-server")
    .WaitFor(authDb)
    .WaitFor(nats)
    .WithReference(authDb)
    .WithReference(nats)
    .WithEndpoint("socket-server", e =>
    {
        e.UriScheme = "tcp";
        e.Port = 5555;
        e.TargetPort = 5555;
        e.IsExternal = true;
        e.IsProxied = false;
        e.Protocol = ProtocolType.Tcp;
    });

auth.WithEnvironment("ConnectionStrings__socket-server", auth.GetEndpoint("socket-server"));

var game = builder
    .AddProject<Krosmoz_Servers_GameServer>("krosmoz-game-server")
    .WaitFor(gameDb)
    .WaitFor(auth)
    .WaitFor(nats)
    .WithReference(gameDb)
    .WithReference(nats)
    .WithEndpoint("socket-server", e =>
    {
        e.UriScheme = "tcp";
        e.Port = 5556;
        e.TargetPort = 5556;
        e.IsExternal = true;
        e.IsProxied = false;
        e.Protocol = ProtocolType.Tcp;
    });

game.WithEnvironment("ConnectionStrings__socket-server", game.GetEndpoint("socket-server"));

IResourceBuilder<ProjectResource>? migrator = null;

if (forceMigrations)
{
    migrator = builder
        .AddProject<Krosmoz_Tools_Migrations>("krosmoz-migrations")
        .WaitFor(authDb)
        .WaitFor(gameDb)
        .WithReference(authDb)
        .WithReference(gameDb);

    auth.WaitForCompletion(migrator);
    game.WaitForCompletion(migrator);
}

if (forceSeeding)
{
    var seeder = builder
        .AddProject<Krosmoz_Tools_Seeds>("krosmoz-seeds")
        .WaitFor(authDb)
        .WaitFor(gameDb)
        .WithReference(authDb)
        .WithReference(gameDb);

    if (migrator is not null)
        seeder.WaitForCompletion(migrator);

    auth.WaitForCompletion(seeder);
    game.WaitForCompletion(seeder);
}

builder.Services.AddLogging(static x => x.UseSerilog());

builder
    .Build()
    .Run();
