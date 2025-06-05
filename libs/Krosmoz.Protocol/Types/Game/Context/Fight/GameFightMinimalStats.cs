// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Types.Game.Context.Fight;

public class GameFightMinimalStats : DofusType
{
	public new const ushort StaticProtocolId = 31;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static GameFightMinimalStats Empty =>
		new() { LifePoints = 0, MaxLifePoints = 0, BaseMaxLifePoints = 0, PermanentDamagePercent = 0, ShieldPoints = 0, ActionPoints = 0, MaxActionPoints = 0, MovementPoints = 0, MaxMovementPoints = 0, Summoner = 0, Summoned = false, NeutralElementResistPercent = 0, EarthElementResistPercent = 0, WaterElementResistPercent = 0, AirElementResistPercent = 0, FireElementResistPercent = 0, NeutralElementReduction = 0, EarthElementReduction = 0, WaterElementReduction = 0, AirElementReduction = 0, FireElementReduction = 0, CriticalDamageFixedResist = 0, PushDamageFixedResist = 0, DodgePALostProbability = 0, DodgePMLostProbability = 0, TackleBlock = 0, TackleEvade = 0, InvisibilityState = 0 };

	public required uint LifePoints { get; set; }

	public required uint MaxLifePoints { get; set; }

	public required uint BaseMaxLifePoints { get; set; }

	public required uint PermanentDamagePercent { get; set; }

	public required uint ShieldPoints { get; set; }

	public required short ActionPoints { get; set; }

	public required short MaxActionPoints { get; set; }

	public required short MovementPoints { get; set; }

	public required short MaxMovementPoints { get; set; }

	public required int Summoner { get; set; }

	public required bool Summoned { get; set; }

	public required short NeutralElementResistPercent { get; set; }

	public required short EarthElementResistPercent { get; set; }

	public required short WaterElementResistPercent { get; set; }

	public required short AirElementResistPercent { get; set; }

	public required short FireElementResistPercent { get; set; }

	public required short NeutralElementReduction { get; set; }

	public required short EarthElementReduction { get; set; }

	public required short WaterElementReduction { get; set; }

	public required short AirElementReduction { get; set; }

	public required short FireElementReduction { get; set; }

	public required short CriticalDamageFixedResist { get; set; }

	public required short PushDamageFixedResist { get; set; }

	public required ushort DodgePALostProbability { get; set; }

	public required ushort DodgePMLostProbability { get; set; }

	public required short TackleBlock { get; set; }

	public required short TackleEvade { get; set; }

	public required sbyte InvisibilityState { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteVarUInt32(LifePoints);
		writer.WriteVarUInt32(MaxLifePoints);
		writer.WriteVarUInt32(BaseMaxLifePoints);
		writer.WriteVarUInt32(PermanentDamagePercent);
		writer.WriteVarUInt32(ShieldPoints);
		writer.WriteVarInt16(ActionPoints);
		writer.WriteVarInt16(MaxActionPoints);
		writer.WriteVarInt16(MovementPoints);
		writer.WriteVarInt16(MaxMovementPoints);
		writer.WriteInt32(Summoner);
		writer.WriteBoolean(Summoned);
		writer.WriteVarInt16(NeutralElementResistPercent);
		writer.WriteVarInt16(EarthElementResistPercent);
		writer.WriteVarInt16(WaterElementResistPercent);
		writer.WriteVarInt16(AirElementResistPercent);
		writer.WriteVarInt16(FireElementResistPercent);
		writer.WriteVarInt16(NeutralElementReduction);
		writer.WriteVarInt16(EarthElementReduction);
		writer.WriteVarInt16(WaterElementReduction);
		writer.WriteVarInt16(AirElementReduction);
		writer.WriteVarInt16(FireElementReduction);
		writer.WriteVarInt16(CriticalDamageFixedResist);
		writer.WriteVarInt16(PushDamageFixedResist);
		writer.WriteVarUInt16(DodgePALostProbability);
		writer.WriteVarUInt16(DodgePMLostProbability);
		writer.WriteVarInt16(TackleBlock);
		writer.WriteVarInt16(TackleEvade);
		writer.WriteInt8(InvisibilityState);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		LifePoints = reader.ReadVarUInt32();
		MaxLifePoints = reader.ReadVarUInt32();
		BaseMaxLifePoints = reader.ReadVarUInt32();
		PermanentDamagePercent = reader.ReadVarUInt32();
		ShieldPoints = reader.ReadVarUInt32();
		ActionPoints = reader.ReadVarInt16();
		MaxActionPoints = reader.ReadVarInt16();
		MovementPoints = reader.ReadVarInt16();
		MaxMovementPoints = reader.ReadVarInt16();
		Summoner = reader.ReadInt32();
		Summoned = reader.ReadBoolean();
		NeutralElementResistPercent = reader.ReadVarInt16();
		EarthElementResistPercent = reader.ReadVarInt16();
		WaterElementResistPercent = reader.ReadVarInt16();
		AirElementResistPercent = reader.ReadVarInt16();
		FireElementResistPercent = reader.ReadVarInt16();
		NeutralElementReduction = reader.ReadVarInt16();
		EarthElementReduction = reader.ReadVarInt16();
		WaterElementReduction = reader.ReadVarInt16();
		AirElementReduction = reader.ReadVarInt16();
		FireElementReduction = reader.ReadVarInt16();
		CriticalDamageFixedResist = reader.ReadVarInt16();
		PushDamageFixedResist = reader.ReadVarInt16();
		DodgePALostProbability = reader.ReadVarUInt16();
		DodgePMLostProbability = reader.ReadVarUInt16();
		TackleBlock = reader.ReadVarInt16();
		TackleEvade = reader.ReadVarInt16();
		InvisibilityState = reader.ReadInt8();
	}
}
