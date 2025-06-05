// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Context.Fight;

public sealed class GameFightOptionStateUpdateMessage : DofusMessage
{
	public new const uint StaticProtocolId = 5927;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static GameFightOptionStateUpdateMessage Empty =>
		new() { FightId = 0, TeamId = 0, Option = 0, State = false };

	public required short FightId { get; set; }

	public required sbyte TeamId { get; set; }

	public required sbyte Option { get; set; }

	public required bool State { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteInt16(FightId);
		writer.WriteInt8(TeamId);
		writer.WriteInt8(Option);
		writer.WriteBoolean(State);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		FightId = reader.ReadInt16();
		TeamId = reader.ReadInt8();
		Option = reader.ReadInt8();
		State = reader.ReadBoolean();
	}
}
