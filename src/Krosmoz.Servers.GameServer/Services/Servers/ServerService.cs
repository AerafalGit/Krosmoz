// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Datacenter.Servers;
using Krosmoz.Protocol.Enums;
using Krosmoz.Protocol.Enums.Custom;
using Krosmoz.Protocol.Messages.Game.Approach;
using Krosmoz.Protocol.Messages.Secure;
using Krosmoz.Protocol.Types.Game.Approach;
using Krosmoz.Servers.GameServer.Models.Options.Servers;
using Krosmoz.Servers.GameServer.Network.Transport;
using Krosmoz.Servers.GameServer.Services.Breeds;
using Krosmoz.Servers.GameServer.Services.Commands;
using Krosmoz.Servers.GameServer.Services.Datacenter;
using Krosmoz.Servers.GameServer.Services.OptionalFeatures;
using Microsoft.Extensions.Options;

namespace Krosmoz.Servers.GameServer.Services.Servers;

/// <summary>
/// Provides functionality for managing server-related operations.
/// </summary>
public sealed class ServerService : IServerService
{
    private readonly IOptionalFeatureService _optionalFeatureService;
    private readonly IBreedService _breedService;
    private readonly ICommandService _commandService;
    private readonly sbyte _serverCommunityId;
    private readonly string _serverLanguage;

    /// <summary>
    /// Gets the unique identifier of the server.
    /// </summary>
    public ushort ServerId { get; }

    /// <summary>
    /// Gets the name of the server.
    /// </summary>
    public string ServerName { get; }

    /// <summary>
    /// Gets the game type of the server.
    /// </summary>
    public ServerGameTypeIds ServerGameType { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="ServerService"/> class.
    /// </summary>
    /// <param name="optionalFeatureService">The service for managing optional features.</param>
    /// <param name="breedService">The service for managing breed-related operations.</param>
    /// <param name="commandService">The service for managing commands.</param>
    /// <param name="datacenterService">The service for accessing datacenter data.</param>
    /// <param name="options">The application configuration options.</param>
    public ServerService(
        IOptionalFeatureService optionalFeatureService,
        IBreedService breedService,
        ICommandService commandService,
        IDatacenterService datacenterService,
        IOptions<ServerOptions> options)
    {
        _optionalFeatureService = optionalFeatureService;
        _breedService = breedService;
        _commandService = commandService;

        var server = datacenterService.GetObjects<Server>(true).First(x => x.Id == options.Value.ServerId);

        _serverCommunityId = (sbyte)server.CommunityId;
        _serverLanguage = server.Language;

        ServerId = (ushort)server.Id;
        ServerName = server.Name;
        ServerGameType = (ServerGameTypeIds)server.GameTypeId;
    }

    /// <summary>
    /// Sends server information asynchronously to the specified game connection.
    /// </summary>
    /// <param name="connection">The game connection to send the server information to.</param>
    public async Task SendServerInformationsAsync(DofusConnection connection)
    {
        await SendServerSettingsAsync(connection);
        await SendServerOptionalFeaturesAsync(connection);
        await SendServerconnectionConstantsAsync(connection);
        await SendAccountCapabilitiesAsync(connection);
        await SendTrustStatusAsync(connection);
        await SendConsoleCommandListAsync(connection);
    }

    /// <summary>
    /// Sends server settings information asynchronously to the specified game connection.
    /// </summary>
    /// <param name="connection">The game connection to which the settings will be sent.</param>
    private ValueTask SendServerSettingsAsync(DofusConnection connection)
    {
        return connection.SendAsync(new ServerSettingsMessage
        {
            Community = _serverCommunityId,
            Lang = _serverLanguage,
            GameType = (sbyte)ServerGameType
        });
    }

    /// <summary>
    /// Sends the list of enabled optional features asynchronously to the specified game connection.
    /// </summary>
    /// <param name="connection">The game connection to which the features will be sent.</param>
    private ValueTask SendServerOptionalFeaturesAsync(DofusConnection connection)
    {
        return connection.SendAsync(new ServerOptionalFeaturesMessage
        {
            Features = _optionalFeatureService.GetEnabledFeatures().Select(static x => (sbyte)x)
        });
    }

    /// <summary>
    /// Sends server connection constants asynchronously to the specified game connection.
    /// </summary>
    /// <param name="connection">The game connection to which the constants will be sent.</param>
    private static ValueTask SendServerconnectionConstantsAsync(DofusConnection connection)
    {
        return connection.SendAsync(new ServerSessionConstantsMessage
        {
            Variables =
            [
                new ServerSessionConstantInteger
                {
                    Id = (ushort)ServerSessionConstants.TimeBeforeDisconnection,
                    Value = (int)TimeSpan.FromMinutes(5).TotalMilliseconds
                }
            ]
        });
    }

    /// <summary>
    /// Sends account capabilities information asynchronously to the specified game connection.
    /// </summary>
    /// <param name="connection">The game connection to which the account capabilities will be sent.</param>
    private ValueTask SendAccountCapabilitiesAsync(DofusConnection connection)
    {
        return connection.SendAsync(new AccountCapabilitiesMessage
        {
            AccountId = connection.Heroes.Account.Id,
            Status = (sbyte)connection.Heroes.Account.Hierarchy,
            TutorialAvailable = true,
            CanCreateNewCharacter = true,
            BreedsVisible = _breedService.GetVisibleBreedsFlags(),
            BreedsAvailable = _breedService.GetPlayableBreedsFlags()
        });
    }

    /// <summary>
    /// Sends trust status information asynchronously to the specified game connection.
    /// </summary>
    /// <param name="connection">The game connection to which the trust status will be sent.</param>
    private static ValueTask SendTrustStatusAsync(DofusConnection connection)
    {
        return connection.SendAsync(new TrustStatusMessage
        {
            Trusted = true,
            Certified = true
        });
    }

    /// <summary>
    /// Sends the list of console commands asynchronously to the specified game connection.
    /// </summary>
    /// <param name="connection">The game connection to which the console commands will be sent.</param>
    private ValueTask SendConsoleCommandListAsync(DofusConnection connection)
    {
        return connection.Heroes.Account.Hierarchy < GameHierarchies.Moderator
            ? ValueTask.CompletedTask
            : _commandService.SendConsoleCommandsListAsync(connection);
    }
}
