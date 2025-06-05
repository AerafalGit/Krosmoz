// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Context.Roleplay.Quest;

public sealed class QuestObjectiveValidationMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6085;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static QuestObjectiveValidationMessage Empty =>
		new() { QuestId = 0, ObjectiveId = 0 };

	public required ushort QuestId { get; set; }

	public required ushort ObjectiveId { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteVarUInt16(QuestId);
		writer.WriteVarUInt16(ObjectiveId);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		QuestId = reader.ReadVarUInt16();
		ObjectiveId = reader.ReadVarUInt16();
	}
}
