// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Game.Prism;
using Krosmoz.Protocol.Types.Game.Social;

namespace Krosmoz.Protocol.Messages.Game.Alliance;

public sealed class AllianceInsiderInfoMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6403;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static AllianceInsiderInfoMessage Empty =>
		new() { AllianceInfos = AllianceFactSheetInformations.Empty, Guilds = [], Prisms = [] };

	public required AllianceFactSheetInformations AllianceInfos { get; set; }

	public required IEnumerable<GuildInsiderFactSheetInformations> Guilds { get; set; }

	public required IEnumerable<PrismSubareaEmptyInfo> Prisms { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		AllianceInfos.Serialize(writer);
		var guildsBefore = writer.Position;
		var guildsCount = 0;
		writer.WriteShort(0);
		foreach (var item in Guilds)
		{
			item.Serialize(writer);
			guildsCount++;
		}
		var guildsAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, guildsBefore);
		writer.WriteShort((short)guildsCount);
		writer.Seek(SeekOrigin.Begin, guildsAfter);
		var prismsBefore = writer.Position;
		var prismsCount = 0;
		writer.WriteShort(0);
		foreach (var item in Prisms)
		{
			writer.WriteUShort(item.ProtocolId);
			item.Serialize(writer);
			prismsCount++;
		}
		var prismsAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, prismsBefore);
		writer.WriteShort((short)prismsCount);
		writer.Seek(SeekOrigin.Begin, prismsAfter);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		AllianceInfos = AllianceFactSheetInformations.Empty;
		AllianceInfos.Deserialize(reader);
		var guildsCount = reader.ReadShort();
		var guilds = new GuildInsiderFactSheetInformations[guildsCount];
		for (var i = 0; i < guildsCount; i++)
		{
			var entry = GuildInsiderFactSheetInformations.Empty;
			entry.Deserialize(reader);
			guilds[i] = entry;
		}
		Guilds = guilds;
		var prismsCount = reader.ReadShort();
		var prisms = new PrismSubareaEmptyInfo[prismsCount];
		for (var i = 0; i < prismsCount; i++)
		{
			var entry = Types.TypeFactory.CreateType<PrismSubareaEmptyInfo>(reader.ReadUShort());
			entry.Deserialize(reader);
			prisms[i] = entry;
		}
		Prisms = prisms;
	}
}
