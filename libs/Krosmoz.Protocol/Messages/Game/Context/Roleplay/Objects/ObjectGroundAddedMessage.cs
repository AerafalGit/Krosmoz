// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Context.Roleplay.Objects;

public sealed class ObjectGroundAddedMessage : DofusMessage
{
	public new const uint StaticProtocolId = 3017;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static ObjectGroundAddedMessage Empty =>
		new() { CellId = 0, ObjectGID = 0 };

	public required short CellId { get; set; }

	public required short ObjectGID { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteShort(CellId);
		writer.WriteShort(ObjectGID);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		CellId = reader.ReadShort();
		ObjectGID = reader.ReadShort();
	}
}
