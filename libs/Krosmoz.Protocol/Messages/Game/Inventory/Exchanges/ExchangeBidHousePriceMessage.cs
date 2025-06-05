// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Inventory.Exchanges;

public sealed class ExchangeBidHousePriceMessage : DofusMessage
{
	public new const uint StaticProtocolId = 5805;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static ExchangeBidHousePriceMessage Empty =>
		new() { GenId = 0 };

	public required ushort GenId { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteVarUInt16(GenId);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		GenId = reader.ReadVarUInt16();
	}
}
