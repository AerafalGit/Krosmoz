// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Enums.Custom;

namespace Krosmoz.Servers.GameServer.Services.OptionalFeatures;

/// <summary>
/// Defines the contract for a service that manages optional features.
/// </summary>
public interface IOptionalFeatureService
{
    /// <summary>
    /// Retrieves a collection of enabled optional features.
    /// </summary>
    /// <returns>
    /// An <see cref="IEnumerable{T}"/> containing the IDs of the enabled optional features.
    /// </returns>
    IEnumerable<OptionalFeatureIds> GetEnabledFeatures();

    /// <summary>
    /// Determines whether a specific optional feature is enabled.
    /// </summary>
    /// <param name="featureId">The ID of the optional feature to check.</param>
    /// <returns>
    /// <c>true</c> if the feature is enabled; otherwise, <c>false</c>.
    /// </returns>
    bool IsFeatureEnabled(OptionalFeatureIds featureId);
}
