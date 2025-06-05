// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Messages.Game.Inventory.Items;

namespace Krosmoz.Protocol.Messages.Game.Inventory.Storage;

public sealed class StorageInventoryContentMessage : InventoryContentMessage
{
	public new const uint StaticProtocolId = 5646;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static new StorageInventoryContentMessage Empty =>
		new() { Kamas = 0, Objects = [] };
}
