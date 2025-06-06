// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using System.Diagnostics.CodeAnalysis;
using Krosmoz.Protocol.Datacenter.Breeds;
using Krosmoz.Protocol.Enums.Custom;

namespace Krosmoz.Servers.GameServer.Services.Breeds;

/// <summary>
/// Defines the contract for a service that provides information about playable breeds.
/// </summary>
public interface IBreedService
{
    /// <summary>
    /// Gets the flags representing the visible breeds.
    /// </summary>
    /// <returns>A short containing the flags for visible breeds.</returns>
    ushort GetVisibleBreedsFlags();

    /// <summary>
    /// Gets the flags representing the playable breeds.
    /// </summary>
    /// <returns>A short containing the flags for playable breeds.</returns>
    ushort GetPlayableBreedsFlags();

    /// <summary>
    /// Attempts to retrieve breed information based on the specified breed ID.
    /// </summary>
    /// <param name="breedId">The ID of the breed to retrieve.</param>
    /// <param name="breed">When the method returns, contains the breed if found; otherwise, <c>null</c>.</param>
    /// <returns><c>true</c> if the breed was found; otherwise, <c>false</c>.</returns>
    bool TryGetBreed(BreedIds breedId, [NotNullWhen(true)] out Breed? breed);

    /// <summary>
    /// Attempts to retrieve head information based on the specified head ID.
    /// </summary>
    /// <param name="headId">The ID of the head to retrieve.</param>
    /// <param name="head">When the method returns, contains the head if found; otherwise, <c>null</c>.</param>
    /// <returns><c>true</c> if the head was found; otherwise, <c>false</c>.</returns>
    bool TryGetHead(int headId, [NotNullWhen(true)] out Head? head);
}
