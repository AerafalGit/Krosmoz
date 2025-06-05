// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Inventory.Exchanges;

public sealed class ExchangeBidHouseInListRemovedMessage : DofusMessage
{
	public new const uint StaticProtocolId = 5950;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static ExchangeBidHouseInListRemovedMessage Empty =>
		new() { ItemUID = 0 };

	public required int ItemUID { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteInt32(ItemUID);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		ItemUID = reader.ReadInt32();
	}
}
