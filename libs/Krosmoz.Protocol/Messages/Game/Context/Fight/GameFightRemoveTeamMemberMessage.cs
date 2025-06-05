// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Context.Fight;

public sealed class GameFightRemoveTeamMemberMessage : DofusMessage
{
	public new const uint StaticProtocolId = 711;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static GameFightRemoveTeamMemberMessage Empty =>
		new() { FightId = 0, TeamId = 0, CharId = 0 };

	public required short FightId { get; set; }

	public required sbyte TeamId { get; set; }

	public required int CharId { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteInt16(FightId);
		writer.WriteInt8(TeamId);
		writer.WriteInt32(CharId);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		FightId = reader.ReadInt16();
		TeamId = reader.ReadInt8();
		CharId = reader.ReadInt32();
	}
}
