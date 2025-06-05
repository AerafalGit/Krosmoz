// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Types.Game.Context.Fight;

public sealed class FightTeamLightInformations : AbstractFightTeamInformations
{
	public new const ushort StaticProtocolId = 115;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static new FightTeamLightInformations Empty =>
		new() { NbWaves = 0, TeamTypeId = 0, TeamSide = 0, LeaderId = 0, TeamId = 0, HasFriend = false, HasGuildMember = false, HasAllianceMember = false, HasGroupMember = false, HasMyTaxCollector = false, TeamMembersCount = 0, MeanLevel = 0 };

	public required bool HasFriend { get; set; }

	public required bool HasGuildMember { get; set; }

	public required bool HasAllianceMember { get; set; }

	public required bool HasGroupMember { get; set; }

	public required bool HasMyTaxCollector { get; set; }

	public required sbyte TeamMembersCount { get; set; }

	public required uint MeanLevel { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		var flag = new byte();
		flag = BooleanByteWrapper.SetFlag(flag, 0, HasFriend);
		flag = BooleanByteWrapper.SetFlag(flag, 1, HasGuildMember);
		flag = BooleanByteWrapper.SetFlag(flag, 2, HasAllianceMember);
		flag = BooleanByteWrapper.SetFlag(flag, 3, HasGroupMember);
		flag = BooleanByteWrapper.SetFlag(flag, 4, HasMyTaxCollector);
		writer.WriteUInt8(flag);
		writer.WriteInt8(TeamMembersCount);
		writer.WriteVarUInt32(MeanLevel);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		var flag = reader.ReadUInt8();
		HasFriend = BooleanByteWrapper.GetFlag(flag, 0);
		HasGuildMember = BooleanByteWrapper.GetFlag(flag, 1);
		HasAllianceMember = BooleanByteWrapper.GetFlag(flag, 2);
		HasGroupMember = BooleanByteWrapper.GetFlag(flag, 3);
		HasMyTaxCollector = BooleanByteWrapper.GetFlag(flag, 4);
		TeamMembersCount = reader.ReadInt8();
		MeanLevel = reader.ReadVarUInt32();
	}
}
