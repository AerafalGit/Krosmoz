// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Inventory.Exchanges;

public sealed class ExchangeBidHouseGenericItemAddedMessage : DofusMessage
{
	public new const uint StaticProtocolId = 5947;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static ExchangeBidHouseGenericItemAddedMessage Empty =>
		new() { ObjGenericId = 0 };

	public required ushort ObjGenericId { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteVarUInt16(ObjGenericId);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		ObjGenericId = reader.ReadVarUInt16();
	}
}
