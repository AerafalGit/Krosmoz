// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Types.Game.Context.Roleplay.Party;

public sealed class NamedPartyTeamWithOutcome : DofusType
{
	public new const ushort StaticProtocolId = 470;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static NamedPartyTeamWithOutcome Empty =>
		new() { Team = NamedPartyTeam.Empty, Outcome = 0 };

	public required NamedPartyTeam Team { get; set; }

	public required ushort Outcome { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		Team.Serialize(writer);
		writer.WriteVarUInt16(Outcome);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		Team = NamedPartyTeam.Empty;
		Team.Deserialize(reader);
		Outcome = reader.ReadVarUInt16();
	}
}
