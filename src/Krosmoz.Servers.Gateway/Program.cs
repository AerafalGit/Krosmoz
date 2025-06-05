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
    .WithEnvironment("SERVER_PORT", "5555");

var game = builder
    .AddProject<Krosmoz_Servers_GameServer>("krosmoz-game-server")
    .WaitFor(gameDb)
    .WaitFor(auth)
    .WaitFor(nats)
    .WithReference(gameDb)
    .WithReference(nats)
    .WithEnvironment("SERVER_PORT", "5556");

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
