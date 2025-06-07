using Krosmoz.Core.Extensions;
using Krosmoz.Servers.AuthServer.Database;
using Krosmoz.Servers.GameServer.Database;
using Krosmoz.Servers.GameServer.Services.Datacenter;
using Krosmoz.Tools.Seeds;
using Krosmoz.Tools.Seeds.Base;
using Krosmoz.Tools.Seeds.Experiences;
using Krosmoz.Tools.Seeds.Items;
using Krosmoz.Tools.Seeds.Maps;
using Krosmoz.Tools.Seeds.Servers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = Host.CreateApplicationBuilder(args);

builder
    .AddServiceDefaults()
    .UseNpgsqlDbContext<AuthDbContext>("krosmoz-auth-db")
    .UseNpgsqlDbContext<GameDbContext>("krosmoz-game-db");

builder.Services
    .AddSingleton<IDatacenterService, DatacenterService>()
    .AddHostedService<SeedWorker>()
    .AddScoped<BaseSeeder, ExperienceSeeder>()
    .AddScoped<BaseSeeder, ItemAppearanceSeeder>()
    .AddScoped<BaseSeeder, MapSeeder>()
    .AddScoped<BaseSeeder, ServerSeeder>()
    .AddHttpClient("DofusBook", static client => client.BaseAddress = new Uri("https://www.dofusbook.net"));

var app = builder.Build();

app.Run();
