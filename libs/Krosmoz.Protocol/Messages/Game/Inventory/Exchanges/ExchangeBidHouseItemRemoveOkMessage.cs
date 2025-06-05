// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Inventory.Exchanges;

public sealed class ExchangeBidHouseItemRemoveOkMessage : DofusMessage
{
	public new const uint StaticProtocolId = 5946;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static ExchangeBidHouseItemRemoveOkMessage Empty =>
		new() { SellerId = 0 };

	public required int SellerId { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteInt32(SellerId);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		SellerId = reader.ReadInt32();
	}
}
