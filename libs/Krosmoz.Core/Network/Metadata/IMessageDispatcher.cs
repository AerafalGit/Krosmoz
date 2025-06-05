// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Core.Network.Dispatcher;

namespace Krosmoz.Core.Network.Metadata;

/// <summary>
/// Defines a dispatcher interface for handling messages of type <see cref="DofusMessage"/>
/// for a specific connection type.
/// </summary>
/// <typeparam name="TConnection">
/// The type of the connection associated with the message dispatcher.
/// Must be a reference type.
/// </typeparam>
public interface IMessageDispatcher<in TConnection> : IMessageDispatcher<TConnection, DofusMessage>
    where TConnection : class;
