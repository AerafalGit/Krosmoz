// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Types.Game.Context.Fight;

public sealed class GameFightMinimalStatsPreparation : GameFightMinimalStats
{
	public new const ushort StaticProtocolId = 360;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static new GameFightMinimalStatsPreparation Empty =>
		new() { InvisibilityState = 0, TackleEvade = 0, TackleBlock = 0, DodgePMLostProbability = 0, DodgePALostProbability = 0, PushDamageFixedResist = 0, CriticalDamageFixedResist = 0, FireElementReduction = 0, AirElementReduction = 0, WaterElementReduction = 0, EarthElementReduction = 0, NeutralElementReduction = 0, FireElementResistPercent = 0, AirElementResistPercent = 0, WaterElementResistPercent = 0, EarthElementResistPercent = 0, NeutralElementResistPercent = 0, Summoned = false, Summoner = 0, MaxMovementPoints = 0, MovementPoints = 0, MaxActionPoints = 0, ActionPoints = 0, ShieldPoints = 0, PermanentDamagePercent = 0, BaseMaxLifePoints = 0, MaxLifePoints = 0, LifePoints = 0, Initiative = 0 };

	public required uint Initiative { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		writer.WriteVarUInt32(Initiative);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		Initiative = reader.ReadVarUInt32();
	}
}
