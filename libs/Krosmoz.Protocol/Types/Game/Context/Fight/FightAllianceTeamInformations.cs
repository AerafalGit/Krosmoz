// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Types.Game.Context.Fight;

public sealed class FightAllianceTeamInformations : FightTeamInformations
{
	public new const ushort StaticProtocolId = 439;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static new FightAllianceTeamInformations Empty =>
		new() { NbWaves = 0, TeamTypeId = 0, TeamSide = 0, LeaderId = 0, TeamId = 0, TeamMembers = [], Relation = 0 };

	public required sbyte Relation { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		writer.WriteInt8(Relation);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		Relation = reader.ReadInt8();
	}
}
