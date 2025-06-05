// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Inventory.Exchanges;

public class ExchangeCraftResultWithObjectIdMessage : ExchangeCraftResultMessage
{
	public new const uint StaticProtocolId = 6000;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static new ExchangeCraftResultWithObjectIdMessage Empty =>
		new() { CraftResult = 0, ObjectGenericId = 0 };

	public required ushort ObjectGenericId { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		writer.WriteVarUInt16(ObjectGenericId);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		ObjectGenericId = reader.ReadVarUInt16();
	}
}
