// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.SourceGenerators.MessageHandler.Models;

/// <summary>
/// Represents a symbol for a message handler, containing the handler's name and the associated message type name.
/// </summary>
/// <param name="MessageHandlerName">The name of the message handler.</param>
/// <param name="MessageTypeName">The name of the message type associated with the handler.</param>
public sealed record MessageHandlerSymbol(string MessageHandlerName, string MessageTypeName);
