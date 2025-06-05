// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Game.Social;

namespace Krosmoz.Protocol.Messages.Game.Guild;

public sealed class GuildVersatileInfoListMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6435;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static GuildVersatileInfoListMessage Empty =>
		new() { Guilds = [] };

	public required IEnumerable<GuildVersatileInformations> Guilds { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		var guildsBefore = writer.Position;
		var guildsCount = 0;
		writer.WriteInt16(0);
		foreach (var item in Guilds)
		{
			writer.WriteUInt16(item.ProtocolId);
			item.Serialize(writer);
			guildsCount++;
		}
		var guildsAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, guildsBefore);
		writer.WriteInt16((short)guildsCount);
		writer.Seek(SeekOrigin.Begin, guildsAfter);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		var guildsCount = reader.ReadInt16();
		var guilds = new GuildVersatileInformations[guildsCount];
		for (var i = 0; i < guildsCount; i++)
		{
			var entry = Types.TypeFactory.CreateType<GuildVersatileInformations>(reader.ReadUInt16());
			entry.Deserialize(reader);
			guilds[i] = entry;
		}
		Guilds = guilds;
	}
}
