// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Types.Game.Context.Roleplay.Quest;

public class QuestObjectiveInformations : DofusType
{
	public new const ushort StaticProtocolId = 385;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static QuestObjectiveInformations Empty =>
		new() { ObjectiveId = 0, ObjectiveStatus = false, DialogParams = [] };

	public required short ObjectiveId { get; set; }

	public required bool ObjectiveStatus { get; set; }

	public required IEnumerable<string> DialogParams { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteShort(ObjectiveId);
		writer.WriteBoolean(ObjectiveStatus);
		var dialogParamsBefore = writer.Position;
		var dialogParamsCount = 0;
		writer.WriteShort(0);
		foreach (var item in DialogParams)
		{
			writer.WriteUtfLengthPrefixed16(item);
			dialogParamsCount++;
		}
		var dialogParamsAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, dialogParamsBefore);
		writer.WriteShort((short)dialogParamsCount);
		writer.Seek(SeekOrigin.Begin, dialogParamsAfter);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		ObjectiveId = reader.ReadShort();
		ObjectiveStatus = reader.ReadBoolean();
		var dialogParamsCount = reader.ReadShort();
		var dialogParams = new string[dialogParamsCount];
		for (var i = 0; i < dialogParamsCount; i++)
		{
			dialogParams[i] = reader.ReadUtfLengthPrefixed16();
		}
		DialogParams = dialogParams;
	}
}
