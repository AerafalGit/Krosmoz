// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Messages.Game.Inventory.Exchanges;

namespace Krosmoz.Protocol.Messages.Game.Inventory.Items;

public sealed class ExchangeKamaModifiedMessage : ExchangeObjectMessage
{
	public new const uint StaticProtocolId = 5521;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static new ExchangeKamaModifiedMessage Empty =>
		new() { Remote = false, Quantity = 0 };

	public required uint Quantity { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		writer.WriteVarUInt32(Quantity);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		Quantity = reader.ReadVarUInt32();
	}
}
