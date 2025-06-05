// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Web.Ankabox;

public sealed class NewMailMessage : MailStatusMessage
{
	public new const uint StaticProtocolId = 6292;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static new NewMailMessage Empty =>
		new() { Total = 0, Unread = 0, SendersAccountId = [] };

	public required IEnumerable<int> SendersAccountId { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		var sendersAccountIdBefore = writer.Position;
		var sendersAccountIdCount = 0;
		writer.WriteInt16(0);
		foreach (var item in SendersAccountId)
		{
			writer.WriteInt32(item);
			sendersAccountIdCount++;
		}
		var sendersAccountIdAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, sendersAccountIdBefore);
		writer.WriteInt16((short)sendersAccountIdCount);
		writer.Seek(SeekOrigin.Begin, sendersAccountIdAfter);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		var sendersAccountIdCount = reader.ReadInt16();
		var sendersAccountId = new int[sendersAccountIdCount];
		for (var i = 0; i < sendersAccountIdCount; i++)
		{
			sendersAccountId[i] = reader.ReadInt32();
		}
		SendersAccountId = sendersAccountId;
	}
}
