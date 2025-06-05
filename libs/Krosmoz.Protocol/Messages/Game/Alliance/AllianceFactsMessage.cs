// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Game.Context.Roleplay;
using Krosmoz.Protocol.Types.Game.Social;

namespace Krosmoz.Protocol.Messages.Game.Alliance;

public sealed class AllianceFactsMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6414;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static AllianceFactsMessage Empty =>
		new() { Infos = AllianceFactSheetInformations.Empty, Guilds = [], ControlledSubareaIds = [], LeaderCharacterId = 0, LeaderCharacterName = string.Empty };

	public required AllianceFactSheetInformations Infos { get; set; }

	public required IEnumerable<GuildInAllianceInformations> Guilds { get; set; }

	public required IEnumerable<ushort> ControlledSubareaIds { get; set; }

	public required uint LeaderCharacterId { get; set; }

	public required string LeaderCharacterName { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteUInt16(Infos.ProtocolId);
		Infos.Serialize(writer);
		var guildsBefore = writer.Position;
		var guildsCount = 0;
		writer.WriteInt16(0);
		foreach (var item in Guilds)
		{
			item.Serialize(writer);
			guildsCount++;
		}
		var guildsAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, guildsBefore);
		writer.WriteInt16((short)guildsCount);
		writer.Seek(SeekOrigin.Begin, guildsAfter);
		var controlledSubareaIdsBefore = writer.Position;
		var controlledSubareaIdsCount = 0;
		writer.WriteInt16(0);
		foreach (var item in ControlledSubareaIds)
		{
			writer.WriteVarUInt16(item);
			controlledSubareaIdsCount++;
		}
		var controlledSubareaIdsAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, controlledSubareaIdsBefore);
		writer.WriteInt16((short)controlledSubareaIdsCount);
		writer.Seek(SeekOrigin.Begin, controlledSubareaIdsAfter);
		writer.WriteVarUInt32(LeaderCharacterId);
		writer.WriteUtfPrefixedLength16(LeaderCharacterName);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		Infos = Types.TypeFactory.CreateType<AllianceFactSheetInformations>(reader.ReadUInt16());
		Infos.Deserialize(reader);
		var guildsCount = reader.ReadInt16();
		var guilds = new GuildInAllianceInformations[guildsCount];
		for (var i = 0; i < guildsCount; i++)
		{
			var entry = GuildInAllianceInformations.Empty;
			entry.Deserialize(reader);
			guilds[i] = entry;
		}
		Guilds = guilds;
		var controlledSubareaIdsCount = reader.ReadInt16();
		var controlledSubareaIds = new ushort[controlledSubareaIdsCount];
		for (var i = 0; i < controlledSubareaIdsCount; i++)
		{
			controlledSubareaIds[i] = reader.ReadVarUInt16();
		}
		ControlledSubareaIds = controlledSubareaIds;
		LeaderCharacterId = reader.ReadVarUInt32();
		LeaderCharacterName = reader.ReadUtfPrefixedLength16();
	}
}
