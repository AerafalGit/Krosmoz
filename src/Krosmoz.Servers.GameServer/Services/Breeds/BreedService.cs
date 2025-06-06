// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using System.Collections.Frozen;
using System.Diagnostics.CodeAnalysis;
using Krosmoz.Core.Services;
using Krosmoz.Protocol.Datacenter.Breeds;
using Krosmoz.Protocol.Enums.Custom;
using Krosmoz.Servers.GameServer.Models.Options.Breeds;
using Krosmoz.Servers.GameServer.Services.Datacenter;
using Microsoft.Extensions.Options;

namespace Krosmoz.Servers.GameServer.Services.Breeds;

/// <summary>
/// Provides functionality for managing breed-related operations in the game server.
/// </summary>
public sealed class BreedService : IBreedService, IInitializableService
{
    private readonly IDatacenterService _datacenterService;
    private readonly IOptionsMonitor<BreedOptions> _options;

    private FrozenDictionary<BreedIds, Breed> _breeds;
    private FrozenDictionary<int, Head> _heads;

    /// <summary>
    /// Initializes a new instance of the <see cref="BreedService"/> class.
    /// </summary>
    /// <param name="datacenterService">The datacenter service for accessing breed data.</param>
    /// <param name="options">The options monitor for retrieving breed settings.</param>
    public BreedService(IDatacenterService datacenterService, IOptionsMonitor<BreedOptions> options)
    {
        _datacenterService = datacenterService;
        _options = options;
        _breeds = FrozenDictionary<BreedIds, Breed>.Empty;
        _heads = FrozenDictionary<int, Head>.Empty;
    }

    /// <summary>
    /// Calculates and retrieves the flags representing visible breeds.
    /// </summary>
    /// <returns>A short value representing the visible breeds flags.</returns>
    public ushort GetVisibleBreedsFlags()
    {
        return (ushort)_options.CurrentValue.VisibleBreeds.Aggregate(0, static (current, breed) => current | 1 << (int)breed - 1);
    }

    /// <summary>
    /// Calculates and retrieves the flags representing playable breeds.
    /// </summary>
    /// <returns>A short value representing the playable breeds flags.</returns>
    public ushort GetPlayableBreedsFlags()
    {
        return (ushort)_options.CurrentValue.PlayableBreeds.Aggregate(0, static (current, breed) => current | 1 << (int)breed - 1);
    }

    /// <summary>
    /// Attempts to retrieve breed information based on the specified breed ID.
    /// </summary>
    /// <param name="breedId">The ID of the breed to retrieve.</param>
    /// <param name="breed">When the method returns, contains the breed if found; otherwise, <c>null</c>.</param>
    /// <returns><c>true</c> if the breed was found; otherwise, <c>false</c>.</returns>
    public bool TryGetBreed(BreedIds breedId, [NotNullWhen(true)] out Breed? breed)
    {
        return _breeds.TryGetValue(breedId, out breed);
    }

    /// <summary>
    /// Attempts to retrieve head information based on the specified head ID.
    /// </summary>
    /// <param name="headId">The ID of the head to retrieve.</param>
    /// <param name="head">When the method returns, contains the head if found; otherwise, <c>null</c>.</param>
    /// <returns><c>true</c> if the head was found; otherwise, <c>false</c>.</returns>
    public bool TryGetHead(int headId, [NotNullWhen(true)] out Head? head)
    {
        return _heads.TryGetValue(headId, out head);
    }

    /// <summary>
    /// Initializes the breed service by loading breed data from the datacenter.
    /// </summary>
    public void Initialize()
    {
        _breeds = _datacenterService.GetObjects<Breed>(true).ToFrozenDictionary(static x => (BreedIds)x.Id);
        _heads = _datacenterService.GetObjects<Head>(true).ToFrozenDictionary(static x => x.Id);
    }
}
