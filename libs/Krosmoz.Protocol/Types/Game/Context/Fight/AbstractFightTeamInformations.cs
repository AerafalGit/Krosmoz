// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Types.Game.Context.Fight;

public class AbstractFightTeamInformations : DofusType
{
	public new const ushort StaticProtocolId = 116;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static AbstractFightTeamInformations Empty =>
		new() { TeamId = 0, LeaderId = 0, TeamSide = 0, TeamTypeId = 0, NbWaves = 0 };

	public required sbyte TeamId { get; set; }

	public required int LeaderId { get; set; }

	public required sbyte TeamSide { get; set; }

	public required sbyte TeamTypeId { get; set; }

	public required sbyte NbWaves { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteInt8(TeamId);
		writer.WriteInt32(LeaderId);
		writer.WriteInt8(TeamSide);
		writer.WriteInt8(TeamTypeId);
		writer.WriteInt8(NbWaves);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		TeamId = reader.ReadInt8();
		LeaderId = reader.ReadInt32();
		TeamSide = reader.ReadInt8();
		TeamTypeId = reader.ReadInt8();
		NbWaves = reader.ReadInt8();
	}
}
