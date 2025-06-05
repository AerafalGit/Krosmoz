// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Inventory.Items;

public sealed class ObjectUseOnCellMessage : ObjectUseMessage
{
	public new const uint StaticProtocolId = 3013;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static new ObjectUseOnCellMessage Empty =>
		new() { ObjectUID = 0, Cells = 0 };

	public required ushort Cells { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		writer.WriteVarUInt16(Cells);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		Cells = reader.ReadVarUInt16();
	}
}
