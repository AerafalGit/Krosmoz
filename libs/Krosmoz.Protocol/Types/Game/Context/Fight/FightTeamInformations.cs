// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Types.Game.Context.Fight;

public class FightTeamInformations : AbstractFightTeamInformations
{
	public new const ushort StaticProtocolId = 33;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static new FightTeamInformations Empty =>
		new() { NbWaves = 0, TeamTypeId = 0, TeamSide = 0, LeaderId = 0, TeamId = 0, TeamMembers = [] };

	public required IEnumerable<FightTeamMemberInformations> TeamMembers { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		var teamMembersBefore = writer.Position;
		var teamMembersCount = 0;
		writer.WriteInt16(0);
		foreach (var item in TeamMembers)
		{
			writer.WriteUInt16(item.ProtocolId);
			item.Serialize(writer);
			teamMembersCount++;
		}
		var teamMembersAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, teamMembersBefore);
		writer.WriteInt16((short)teamMembersCount);
		writer.Seek(SeekOrigin.Begin, teamMembersAfter);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		var teamMembersCount = reader.ReadInt16();
		var teamMembers = new FightTeamMemberInformations[teamMembersCount];
		for (var i = 0; i < teamMembersCount; i++)
		{
			var entry = Types.TypeFactory.CreateType<FightTeamMemberInformations>(reader.ReadUInt16());
			entry.Deserialize(reader);
			teamMembers[i] = entry;
		}
		TeamMembers = teamMembers;
	}
}
