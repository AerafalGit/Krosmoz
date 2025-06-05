// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using System.Collections.Immutable;

namespace Krosmoz.SourceGenerators.MessageDispatcher.Models;

/// <summary>
/// Represents a message handler that contains information about the handler's name,
/// variable name, and the methods it supports for handling specific message types.
/// </summary>
/// <param name="Name">The name of the message handler.</param>
/// <param name="VariableName">The variable name associated with the handler.</param>
/// <param name="Methods">A collection of methods that handle specific message types.</param>
public sealed record MessageHandler(string Name, string VariableName, ImmutableArray<MessageHandlerMethod> Methods);
