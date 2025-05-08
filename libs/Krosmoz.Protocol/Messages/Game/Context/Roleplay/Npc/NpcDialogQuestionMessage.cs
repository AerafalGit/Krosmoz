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

	public required short MessageId { get; set; }

	public required IEnumerable<string> DialogParams { get; set; }

	public required IEnumerable<short> VisibleReplies { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteShort(MessageId);
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
		var visibleRepliesBefore = writer.Position;
		var visibleRepliesCount = 0;
		writer.WriteShort(0);
		foreach (var item in VisibleReplies)
		{
			writer.WriteShort(item);
			visibleRepliesCount++;
		}
		var visibleRepliesAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, visibleRepliesBefore);
		writer.WriteShort((short)visibleRepliesCount);
		writer.Seek(SeekOrigin.Begin, visibleRepliesAfter);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		MessageId = reader.ReadShort();
		var dialogParamsCount = reader.ReadShort();
		var dialogParams = new string[dialogParamsCount];
		for (var i = 0; i < dialogParamsCount; i++)
		{
			dialogParams[i] = reader.ReadUtfLengthPrefixed16();
		}
		DialogParams = dialogParams;
		var visibleRepliesCount = reader.ReadShort();
		var visibleReplies = new short[visibleRepliesCount];
		for (var i = 0; i < visibleRepliesCount; i++)
		{
			visibleReplies[i] = reader.ReadShort();
		}
		VisibleReplies = visibleReplies;
	}
}
