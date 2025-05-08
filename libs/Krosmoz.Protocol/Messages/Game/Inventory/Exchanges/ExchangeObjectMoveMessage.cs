// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Inventory.Exchanges;

public class ExchangeObjectMoveMessage : DofusMessage
{
	public new const uint StaticProtocolId = 5518;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static ExchangeObjectMoveMessage Empty =>
		new() { ObjectUID = 0, Quantity = 0 };

	public required int ObjectUID { get; set; }

	public required int Quantity { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteInt(ObjectUID);
		writer.WriteInt(Quantity);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		ObjectUID = reader.ReadInt();
		Quantity = reader.ReadInt();
	}
}
