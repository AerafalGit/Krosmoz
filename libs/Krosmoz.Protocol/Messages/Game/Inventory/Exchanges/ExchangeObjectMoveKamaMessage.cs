// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Inventory.Exchanges;

public sealed class ExchangeObjectMoveKamaMessage : DofusMessage
{
	public new const uint StaticProtocolId = 5520;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static ExchangeObjectMoveKamaMessage Empty =>
		new() { Quantity = 0 };

	public required int Quantity { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteVarInt32(Quantity);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		Quantity = reader.ReadVarInt32();
	}
}
