// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Game.Context.Roleplay.Party;

namespace Krosmoz.Protocol.Messages.Game.Context.Fight;

public sealed class GameFightSpectatorJoinMessage : GameFightJoinMessage
{
	public new const uint StaticProtocolId = 6504;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static new GameFightSpectatorJoinMessage Empty =>
		new() { FightType = 0, TimeMaxBeforeFightStart = 0, IsFightStarted = false, CanSayReady = false, CanBeCancelled = false, NamedPartyTeams = [] };

	public required IEnumerable<NamedPartyTeam> NamedPartyTeams { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		var namedPartyTeamsBefore = writer.Position;
		var namedPartyTeamsCount = 0;
		writer.WriteInt16(0);
		foreach (var item in NamedPartyTeams)
		{
			item.Serialize(writer);
			namedPartyTeamsCount++;
		}
		var namedPartyTeamsAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, namedPartyTeamsBefore);
		writer.WriteInt16((short)namedPartyTeamsCount);
		writer.Seek(SeekOrigin.Begin, namedPartyTeamsAfter);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		var namedPartyTeamsCount = reader.ReadInt16();
		var namedPartyTeams = new NamedPartyTeam[namedPartyTeamsCount];
		for (var i = 0; i < namedPartyTeamsCount; i++)
		{
			var entry = NamedPartyTeam.Empty;
			entry.Deserialize(reader);
			namedPartyTeams[i] = entry;
		}
		NamedPartyTeams = namedPartyTeams;
	}
}
