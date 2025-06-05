// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Types.Game.Context.Fight;

public class FightResultListEntry : DofusType
{
	public new const ushort StaticProtocolId = 16;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static FightResultListEntry Empty =>
		new() { Outcome = 0, Wave = 0, Rewards = FightLoot.Empty };

	public required ushort Outcome { get; set; }

	public required sbyte Wave { get; set; }

	public required FightLoot Rewards { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteVarUInt16(Outcome);
		writer.WriteInt8(Wave);
		Rewards.Serialize(writer);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		Outcome = reader.ReadVarUInt16();
		Wave = reader.ReadInt8();
		Rewards = FightLoot.Empty;
		Rewards.Deserialize(reader);
	}
}
