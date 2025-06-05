// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Context.Fight;

public sealed class GameFightNewWaveMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6490;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static GameFightNewWaveMessage Empty =>
		new() { Id = 0, TeamId = 0, NbTurnBeforeNextWave = 0 };

	public required sbyte Id { get; set; }

	public required sbyte TeamId { get; set; }

	public required short NbTurnBeforeNextWave { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteInt8(Id);
		writer.WriteInt8(TeamId);
		writer.WriteInt16(NbTurnBeforeNextWave);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		Id = reader.ReadInt8();
		TeamId = reader.ReadInt8();
		NbTurnBeforeNextWave = reader.ReadInt16();
	}
}
