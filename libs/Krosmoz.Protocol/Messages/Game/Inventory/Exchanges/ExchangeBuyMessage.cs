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

	public required uint ObjectToBuyId { get; set; }

	public required uint Quantity { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteVarUInt32(ObjectToBuyId);
		writer.WriteVarUInt32(Quantity);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		ObjectToBuyId = reader.ReadVarUInt32();
		Quantity = reader.ReadVarUInt32();
	}
}
