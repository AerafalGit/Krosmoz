// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Inventory.Exchanges;

public sealed class ExchangeBuyMessage : DofusMessage
{
	public new const uint StaticProtocolId = 5774;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static ExchangeBuyMessage Empty =>
		new() { ObjectToBuyId = 0, Quantity = 0 };

	public required int ObjectToBuyId { get; set; }

	public required int Quantity { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteInt(ObjectToBuyId);
		writer.WriteInt(Quantity);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		ObjectToBuyId = reader.ReadInt();
		Quantity = reader.ReadInt();
	}
}
