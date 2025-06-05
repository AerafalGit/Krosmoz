// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Inventory.Exchanges;

public sealed class ExchangeBidHouseSearchMessage : DofusMessage
{
	public new const uint StaticProtocolId = 5806;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static ExchangeBidHouseSearchMessage Empty =>
		new() { Type = 0, GenId = 0 };

	public required uint Type { get; set; }

	public required ushort GenId { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteVarUInt32(Type);
		writer.WriteVarUInt16(GenId);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		Type = reader.ReadVarUInt32();
		GenId = reader.ReadVarUInt16();
	}
}
