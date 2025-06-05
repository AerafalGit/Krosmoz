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

	public required ushort ObjectiveId { get; set; }

	public required bool ObjectiveStatus { get; set; }

	public required IEnumerable<string> DialogParams { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteVarUInt16(ObjectiveId);
		writer.WriteBoolean(ObjectiveStatus);
		var dialogParamsBefore = writer.Position;
		var dialogParamsCount = 0;
		writer.WriteInt16(0);
		foreach (var item in DialogParams)
		{
			writer.WriteUtfPrefixedLength16(item);
			dialogParamsCount++;
		}
		var dialogParamsAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, dialogParamsBefore);
		writer.WriteInt16((short)dialogParamsCount);
		writer.Seek(SeekOrigin.Begin, dialogParamsAfter);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		ObjectiveId = reader.ReadVarUInt16();
		ObjectiveStatus = reader.ReadBoolean();
		var dialogParamsCount = reader.ReadInt16();
		var dialogParams = new string[dialogParamsCount];
		for (var i = 0; i < dialogParamsCount; i++)
		{
			dialogParams[i] = reader.ReadUtfPrefixedLength16();
		}
		DialogParams = dialogParams;
	}
}
