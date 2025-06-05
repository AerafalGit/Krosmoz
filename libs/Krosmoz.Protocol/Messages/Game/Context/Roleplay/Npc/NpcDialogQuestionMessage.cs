// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Context.Roleplay.Npc;

public sealed class NpcDialogQuestionMessage : DofusMessage
{
	public new const uint StaticProtocolId = 5617;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static NpcDialogQuestionMessage Empty =>
		new() { MessageId = 0, DialogParams = [], VisibleReplies = [] };

	public required ushort MessageId { get; set; }

	public required IEnumerable<string> DialogParams { get; set; }

	public required IEnumerable<ushort> VisibleReplies { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteVarUInt16(MessageId);
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
		var visibleRepliesBefore = writer.Position;
		var visibleRepliesCount = 0;
		writer.WriteInt16(0);
		foreach (var item in VisibleReplies)
		{
			writer.WriteVarUInt16(item);
			visibleRepliesCount++;
		}
		var visibleRepliesAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, visibleRepliesBefore);
		writer.WriteInt16((short)visibleRepliesCount);
		writer.Seek(SeekOrigin.Begin, visibleRepliesAfter);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		MessageId = reader.ReadVarUInt16();
		var dialogParamsCount = reader.ReadInt16();
		var dialogParams = new string[dialogParamsCount];
		for (var i = 0; i < dialogParamsCount; i++)
		{
			dialogParams[i] = reader.ReadUtfPrefixedLength16();
		}
		DialogParams = dialogParams;
		var visibleRepliesCount = reader.ReadInt16();
		var visibleReplies = new ushort[visibleRepliesCount];
		for (var i = 0; i < visibleRepliesCount; i++)
		{
			visibleReplies[i] = reader.ReadVarUInt16();
		}
		VisibleReplies = visibleReplies;
	}
}
