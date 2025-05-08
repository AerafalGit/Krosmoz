// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Inventory.Exchanges;

public sealed class ExchangeBidPriceMessage : DofusMessage
{
	public new const uint StaticProtocolId = 5755;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static ExchangeBidPriceMessage Empty =>
		new() { GenericId = 0, AveragePrice = 0 };

	public required int GenericId { get; set; }

	public required int AveragePrice { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteInt(GenericId);
		writer.WriteInt(AveragePrice);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		GenericId = reader.ReadInt();
		AveragePrice = reader.ReadInt();
	}
}
