// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Types.Game.Context.Roleplay.Quest;

public sealed class QuestObjectiveInformationsWithCompletion : QuestObjectiveInformations
{
	public new const ushort StaticProtocolId = 386;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static new QuestObjectiveInformationsWithCompletion Empty =>
		new() { DialogParams = [], ObjectiveStatus = false, ObjectiveId = 0, CurCompletion = 0, MaxCompletion = 0 };

	public required short CurCompletion { get; set; }

	public required short MaxCompletion { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		writer.WriteInt16(CurCompletion);
		writer.WriteInt16(MaxCompletion);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		CurCompletion = reader.ReadInt16();
		MaxCompletion = reader.ReadInt16();
	}
}
