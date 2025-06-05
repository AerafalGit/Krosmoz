using Krosmoz.Core.Extensions;
using Krosmoz.Servers.AuthServer.Database;
using Krosmoz.Servers.GameServer.Database;
using Krosmoz.Tools.Migrations;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = Host.CreateApplicationBuilder(args);

builder
    .AddServiceDefaults()
    .UseNpgsqlDbContext<AuthDbContext>("krosmoz-auth-db")
    .UseNpgsqlDbContext<GameDbContext>("krosmoz-game-db");

builder.Services.AddHostedService<MigrationWorker>();

var app = builder.Build();

app.Run();
