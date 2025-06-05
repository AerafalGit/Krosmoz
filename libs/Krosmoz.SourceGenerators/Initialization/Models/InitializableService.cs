// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.SourceGenerators.Initialization.Models;

/// <summary>
/// Represents a service that can be initialized with a name and an indication of whether it is asynchronous.
/// </summary>
/// <param name="Name">The name of the service.</param>
/// <param name="IsAsync">A value indicating whether the service is asynchronous.</param>
public sealed record InitializableService(string Name, bool IsAsync);
