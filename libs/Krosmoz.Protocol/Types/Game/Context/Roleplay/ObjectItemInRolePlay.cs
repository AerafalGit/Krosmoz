// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Types.Game.Context.Roleplay;

public class ObjectItemInRolePlay : DofusType
{
	public new const ushort StaticProtocolId = 198;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static ObjectItemInRolePlay Empty =>
		new() { CellId = 0, ObjectGID = 0 };

	public required short CellId { get; set; }

	public required short ObjectGID { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteInt16(CellId);
		writer.WriteInt16(ObjectGID);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		CellId = reader.ReadInt16();
		ObjectGID = reader.ReadInt16();
	}
}
