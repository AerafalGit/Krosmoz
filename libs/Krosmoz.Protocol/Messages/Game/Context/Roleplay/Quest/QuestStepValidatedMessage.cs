// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Context.Roleplay.Quest;

public sealed class QuestStepValidatedMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6099;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static QuestStepValidatedMessage Empty =>
		new() { QuestId = 0, StepId = 0 };

	public required ushort QuestId { get; set; }

	public required ushort StepId { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteVarUInt16(QuestId);
		writer.WriteVarUInt16(StepId);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		QuestId = reader.ReadVarUInt16();
		StepId = reader.ReadVarUInt16();
	}
}
