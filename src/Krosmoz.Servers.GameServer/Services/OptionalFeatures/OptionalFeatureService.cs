// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Enums.Custom;
using Krosmoz.Servers.GameServer.Models.Options.OptionalFeatures;
using Microsoft.Extensions.Options;

namespace Krosmoz.Servers.GameServer.Services.OptionalFeatures;

/// <summary>
/// Provides functionality for managing optional features.
/// </summary>
public sealed class OptionalFeatureService : IOptionalFeatureService
{
    private readonly IOptionsMonitor<OptionalFeaturesOptions> _options;

    /// <summary>
    /// Initializes a new instance of the <see cref="OptionalFeatureService"/> class.
    /// </summary>
    /// <param name="options">The options monitor for accessing optional features configuration.</param>
    public OptionalFeatureService(IOptionsMonitor<OptionalFeaturesOptions> options)
    {
        _options = options;
    }

    /// <summary>
    /// Gets the collection of enabled optional features.
    /// </summary>
    /// <returns>An enumerable of enabled optional feature IDs.</returns>
    public IEnumerable<OptionalFeatureIds> GetEnabledFeatures()
    {
        return _options.CurrentValue.EnabledFeatures;
    }

    /// <summary>
    /// Determines whether a specific optional feature is enabled.
    /// </summary>
    /// <param name="featureId">The ID of the feature to check.</param>
    /// <returns><c>true</c> if the feature is enabled; otherwise, <c>false</c>.</returns>
    public bool IsFeatureEnabled(OptionalFeatureIds featureId)
    {
        return _options.CurrentValue.EnabledFeatures.Contains(featureId);
    }
}
