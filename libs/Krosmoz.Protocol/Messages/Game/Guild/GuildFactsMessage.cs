// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Game.Character;
using Krosmoz.Protocol.Types.Game.Social;

namespace Krosmoz.Protocol.Messages.Game.Guild;

public class GuildFactsMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6415;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static GuildFactsMessage Empty =>
		new() { Infos = GuildFactSheetInformations.Empty, CreationDate = 0, NbTaxCollectors = 0, Enabled = false, Members = [] };

	public required GuildFactSheetInformations Infos { get; set; }

	public required int CreationDate { get; set; }

	public required ushort NbTaxCollectors { get; set; }

	public required bool Enabled { get; set; }

	public required IEnumerable<CharacterMinimalInformations> Members { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteUInt16(Infos.ProtocolId);
		Infos.Serialize(writer);
		writer.WriteInt32(CreationDate);
		writer.WriteVarUInt16(NbTaxCollectors);
		writer.WriteBoolean(Enabled);
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
		Infos = Types.TypeFactory.CreateType<GuildFactSheetInformations>(reader.ReadUInt16());
		Infos.Deserialize(reader);
		CreationDate = reader.ReadInt32();
		NbTaxCollectors = reader.ReadVarUInt16();
		Enabled = reader.ReadBoolean();
		var membersCount = reader.ReadInt16();
		var members = new CharacterMinimalInformations[membersCount];
		for (var i = 0; i < membersCount; i++)
		{
			var entry = CharacterMinimalInformations.Empty;
			entry.Deserialize(reader);
			members[i] = entry;
		}
		Members = members;
	}
}
