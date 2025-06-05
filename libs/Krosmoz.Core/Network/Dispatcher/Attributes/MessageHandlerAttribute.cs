// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Core.Network.Dispatcher.Attributes;

/// <summary>
/// An attribute used to mark methods as message handlers.
/// </summary>
[AttributeUsage(AttributeTargets.Method)]
public sealed class MessageHandlerAttribute : Attribute;
