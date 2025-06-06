// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using System.Collections.Immutable;

namespace Krosmoz.SourceGenerators.Commands.Models;

/// <summary>
/// Represents a group of commands with a name, description, and a collection of commands.
/// </summary>
/// <param name="TypeName">The type name of the command group, typically the class name where the commands are defined.</param>
/// <param name="Name">The name of the command group.</param>
/// <param name="Commands">The collection of commands associated with the group.</param>
public sealed record CommandGroup(string TypeName, string Name, ImmutableArray<Command> Commands);
