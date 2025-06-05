// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Game.Context.Fight;

namespace Krosmoz.Protocol.Messages.Game.Context.Fight;

public sealed class GameFightUpdateTeamMessage : DofusMessage
{
	public new const uint StaticProtocolId = 5572;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static GameFightUpdateTeamMessage Empty =>
		new() { FightId = 0, Team = FightTeamInformations.Empty };

	public required short FightId { get; set; }

	public required FightTeamInformations Team { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteInt16(FightId);
		Team.Serialize(writer);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		FightId = reader.ReadInt16();
		Team = FightTeamInformations.Empty;
		Team.Deserialize(reader);
	}
}
