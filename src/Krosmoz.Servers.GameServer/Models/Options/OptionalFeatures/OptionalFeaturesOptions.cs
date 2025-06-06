// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Enums.Custom;

namespace Krosmoz.Servers.GameServer.Models.Options.OptionalFeatures;

/// <summary>
/// Represents the configuration options for optional features in the game server.
/// </summary>
public sealed class OptionalFeaturesOptions
{
    /// <summary>
    /// Gets or sets the collection of enabled optional feature identifiers.
    /// </summary>
    public required IEnumerable<OptionalFeatureIds> EnabledFeatures { get; set; }
}
