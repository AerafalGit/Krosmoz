// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using System.Collections.Immutable;

namespace Krosmoz.SourceGenerators.Commands.Models;

/// <summary>
/// Represents a command with a name, description, hierarchy, cooldown, and a collection of parameters.
/// </summary>
/// <param name="MethodName">The name of the method associated with the command.</param>
/// <param name="Name">The name of the command.</param>
/// <param name="Description">The description of the command.</param>
/// <param name="Help">The help text for the command, providing additional information.</param>
/// <param name="Hierarchy">The hierarchy level of the command.</param>
/// <param name="Cooldown">The cooldown duration for the command.</param>
/// <param name="Parameters">The collection of parameters associated with the command.</param>
public sealed record Command(string MethodName, string Name, string Description, string Help, CommandHierarchies Hierarchy, TimeSpan Cooldown, ImmutableArray<CommandParameter> Parameters);
