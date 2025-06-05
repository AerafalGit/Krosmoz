// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.SourceGenerators.MessageDispatcher.Models;

/// <summary>
/// Represents a method that handles a specific type of message in the message dispatcher.
/// </summary>
/// <param name="Name">The name of the handler method.</param>
/// <param name="MessageTypeName">The name of the message type that the method handles.</param>
public sealed record MessageHandlerMethod(string Name, string MessageTypeName);
