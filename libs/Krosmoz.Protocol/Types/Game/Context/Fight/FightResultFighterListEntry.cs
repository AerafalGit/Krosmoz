// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Types.Game.Context.Fight;

public class FightResultFighterListEntry : FightResultListEntry
{
	public new const ushort StaticProtocolId = 189;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static new FightResultFighterListEntry Empty =>
		new() { Rewards = FightLoot.Empty, Wave = 0, Outcome = 0, Id = 0, Alive = false };

	public required int Id { get; set; }

	public required bool Alive { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		writer.WriteInt32(Id);
		writer.WriteBoolean(Alive);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		Id = reader.ReadInt32();
		Alive = reader.ReadBoolean();
	}
}
