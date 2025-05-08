// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Types.Game.Context.Fight;

public sealed class FightResultMutantListEntry : FightResultFighterListEntry
{
	public new const ushort StaticProtocolId = 216;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static new FightResultMutantListEntry Empty =>
		new() { Rewards = FightLoot.Empty, Outcome = 0, Alive = false, Id = 0, Level = 0 };

	public required ushort Level { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		writer.WriteUShort(Level);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		Level = reader.ReadUShort();
	}
}
