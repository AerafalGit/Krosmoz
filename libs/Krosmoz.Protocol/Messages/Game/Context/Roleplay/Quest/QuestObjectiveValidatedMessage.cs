// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Context.Roleplay.Quest;

public sealed class QuestObjectiveValidatedMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6098;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static QuestObjectiveValidatedMessage Empty =>
		new() { QuestId = 0, ObjectiveId = 0 };

	public required ushort QuestId { get; set; }

	public required ushort ObjectiveId { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteUInt16(QuestId);
		writer.WriteUInt16(ObjectiveId);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		QuestId = reader.ReadUInt16();
		ObjectiveId = reader.ReadUInt16();
	}
}
