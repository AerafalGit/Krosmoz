// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Context.Fight;

public sealed class GameFightJoinRequestMessage : DofusMessage
{
	public new const uint StaticProtocolId = 701;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static GameFightJoinRequestMessage Empty =>
		new() { FighterId = 0, FightId = 0 };

	public required int FighterId { get; set; }

	public required int FightId { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteInt32(FighterId);
		writer.WriteInt32(FightId);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		FighterId = reader.ReadInt32();
		FightId = reader.ReadInt32();
	}
}
