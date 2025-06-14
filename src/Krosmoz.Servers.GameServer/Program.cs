﻿using Krosmoz.Core.Extensions;
using Krosmoz.Core.Network.Hosting;
using Krosmoz.Core.Network.Protocol.Dofus;
using Krosmoz.Core.Scheduling;
using Krosmoz.Core.Services;
using Krosmoz.Protocol.Ipc;
using Krosmoz.Servers.GameServer.Commands;
using Krosmoz.Servers.GameServer.Database;
using Krosmoz.Servers.GameServer.Factories.Characters;
using Krosmoz.Servers.GameServer.Factories.World;
using Krosmoz.Servers.GameServer.Models.Context;
using Krosmoz.Servers.GameServer.Models.Options.Breeds;
using Krosmoz.Servers.GameServer.Models.Options.Characters;
using Krosmoz.Servers.GameServer.Models.Options.OptionalFeatures;
using Krosmoz.Servers.GameServer.Models.Options.Servers;
using Krosmoz.Servers.GameServer.Network.Transport;
using Krosmoz.Servers.GameServer.Services.Authentication;
using Krosmoz.Servers.GameServer.Services.Characteristics;
using Krosmoz.Servers.GameServer.Services.Characters.Creation;
using Krosmoz.Servers.GameServer.Services.Characters.Creation.NameGeneration;
using Krosmoz.Servers.GameServer.Services.Characters.Deletion;
using Krosmoz.Servers.GameServer.Services.Characters.Loading;
using Krosmoz.Servers.GameServer.Services.Characters.Selection;
using Krosmoz.Servers.GameServer.Services.Chat;
using Krosmoz.Servers.GameServer.Services.Datacenter;
using Krosmoz.Servers.GameServer.Services.InfoMessages;
using Krosmoz.Servers.GameServer.Services.Inventory;
using Krosmoz.Servers.GameServer.Services.Maps.Movements;
using Krosmoz.Servers.GameServer.Services.Maps.Teleport;
using Krosmoz.Servers.GameServer.Services.OptionalFeatures;
using Krosmoz.Servers.GameServer.Services.Servers;
using Krosmoz.Servers.GameServer.Services.Shortcuts;
using Krosmoz.Servers.GameServer.Services.Social;
using Krosmoz.Servers.GameServer.Services.World;
using Microsoft.AspNetCore.Connections;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = Host.CreateApplicationBuilder(args);

builder
    .AddServiceDefaults()
    .UseNpgsqlDbContext<GameDbContext>("krosmoz-game-db")
    .UseNats("krosmoz-nats", IpcJsonSerializerContext.WithCustomConverters)
    .UseServer(static server => server.ListenFromEnvironment(static connection =>
    {
        connection
            .UseTelemetry()
            .UseConnectionHandler<DofusConnectionHandler>();
    }));

builder.Services
    .AddDofusProtocol()
    .AddCommands()
    .AddInitializableServices()
    .Configure<ServerOptions>(builder.Configuration)
    .Configure<OptionalFeaturesOptions>(builder.Configuration)
    .Configure<BreedOptions>(builder.Configuration)
    .Configure<CharacterCreationOptions>(builder.Configuration.GetSection("CharacterCreation"))
    .AddTransient<IScheduler, Scheduler>()
    .AddSingleton<IDatacenterService, DatacenterService>()
    .AddSingleton<IOptionalFeatureService, OptionalFeatureService>()
    .AddSingleton<IServerService, ServerService>()
    .AddScoped<IChatService, ChatService>()
    .AddScoped<IShortcutService, ShortcutService>()
    .AddScoped<IInventoryService, InventoryService>()
    .AddScoped<IInfoMessageService, InfoMessageService>()
    .AddSingleton<IWorldService, WorldService>()
    .AddScoped<ISocialService, SocialService>()
    .AddScoped<IAuthenticationService, AuthenticationService>()
    .AddScoped<ICharacterNameGenerationService, CharacterNameGenerationService>()
    .AddScoped<ICharacterCreationService, CharacterCreationService>()
    .AddScoped<ICharacterSelectionService, CharacterSelectionService>()
    .AddScoped<ICharacterFactory, CharacterFactory>()
    .AddScoped<IWorldPositionFactory, WorldPositionFactory>()
    .AddScoped<IMapMovementService, MapMovementService>()
    .AddScoped<ICharacterDeletionService, CharacterDeletionService>()
    .AddScoped<ICharacterLoadingService, CharacterLoadingService>()
    .AddScoped<IContextService, ContextService>()
    .AddScoped<IMapTeleportService, MapTeleportService>();

builder
    .Build()
    .Run();
