// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Game.Guild;

namespace Krosmoz.Protocol.Messages.Game.Guild;

public sealed class GuildInformationsMembersMessage : DofusMessage
{
	public new const uint StaticProtocolId = 5558;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static GuildInformationsMembersMessage Empty =>
		new() { Members = [] };

	public required IEnumerable<GuildMember> Members { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		var membersBefore = writer.Position;
		var membersCount = 0;
		writer.WriteInt16(0);
		foreach (var item in Members)
		{
			item.Serialize(writer);
			membersCount++;
		}
		var membersAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, membersBefore);
		writer.WriteInt16((short)membersCount);
		writer.Seek(SeekOrigin.Begin, membersAfter);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		var membersCount = reader.ReadInt16();
		var members = new GuildMember[membersCount];
		for (var i = 0; i < membersCount; i++)
		{
			var entry = GuildMember.Empty;
			entry.Deserialize(reader);
			members[i] = entry;
		}
		Members = members;
	}
}
