// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.SourceGenerators.Commands.Models;

/// <summary>
/// Represents a parameter for a command, including its type, name, and additional metadata.
/// </summary>
/// <param name="TypeName">The name of the parameter's type.</param>
/// <param name="Name">The name of the parameter.</param>
/// <param name="IsPrimitive">Indicates whether the parameter is a primitive type.</param>
/// <param name="IsEnum">Indicates whether the parameter is an enumeration type.</param>
/// <param name="IsStruct">Indicates whether the parameter is a struct type.</param>
public sealed record CommandParameter(string TypeName, string Name, bool IsPrimitive, bool IsEnum, bool IsStruct);
