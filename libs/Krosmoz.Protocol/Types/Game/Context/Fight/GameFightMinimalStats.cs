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

	public required int LifePoints { get; set; }

	public required int MaxLifePoints { get; set; }

	public required int BaseMaxLifePoints { get; set; }

	public required int PermanentDamagePercent { get; set; }

	public required int ShieldPoints { get; set; }

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

	public required short DodgePALostProbability { get; set; }

	public required short DodgePMLostProbability { get; set; }

	public required short TackleBlock { get; set; }

	public required short TackleEvade { get; set; }

	public required sbyte InvisibilityState { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteInt(LifePoints);
		writer.WriteInt(MaxLifePoints);
		writer.WriteInt(BaseMaxLifePoints);
		writer.WriteInt(PermanentDamagePercent);
		writer.WriteInt(ShieldPoints);
		writer.WriteShort(ActionPoints);
		writer.WriteShort(MaxActionPoints);
		writer.WriteShort(MovementPoints);
		writer.WriteShort(MaxMovementPoints);
		writer.WriteInt(Summoner);
		writer.WriteBoolean(Summoned);
		writer.WriteShort(NeutralElementResistPercent);
		writer.WriteShort(EarthElementResistPercent);
		writer.WriteShort(WaterElementResistPercent);
		writer.WriteShort(AirElementResistPercent);
		writer.WriteShort(FireElementResistPercent);
		writer.WriteShort(NeutralElementReduction);
		writer.WriteShort(EarthElementReduction);
		writer.WriteShort(WaterElementReduction);
		writer.WriteShort(AirElementReduction);
		writer.WriteShort(FireElementReduction);
		writer.WriteShort(CriticalDamageFixedResist);
		writer.WriteShort(PushDamageFixedResist);
		writer.WriteShort(DodgePALostProbability);
		writer.WriteShort(DodgePMLostProbability);
		writer.WriteShort(TackleBlock);
		writer.WriteShort(TackleEvade);
		writer.WriteSByte(InvisibilityState);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		LifePoints = reader.ReadInt();
		MaxLifePoints = reader.ReadInt();
		BaseMaxLifePoints = reader.ReadInt();
		PermanentDamagePercent = reader.ReadInt();
		ShieldPoints = reader.ReadInt();
		ActionPoints = reader.ReadShort();
		MaxActionPoints = reader.ReadShort();
		MovementPoints = reader.ReadShort();
		MaxMovementPoints = reader.ReadShort();
		Summoner = reader.ReadInt();
		Summoned = reader.ReadBoolean();
		NeutralElementResistPercent = reader.ReadShort();
		EarthElementResistPercent = reader.ReadShort();
		WaterElementResistPercent = reader.ReadShort();
		AirElementResistPercent = reader.ReadShort();
		FireElementResistPercent = reader.ReadShort();
		NeutralElementReduction = reader.ReadShort();
		EarthElementReduction = reader.ReadShort();
		WaterElementReduction = reader.ReadShort();
		AirElementReduction = reader.ReadShort();
		FireElementReduction = reader.ReadShort();
		CriticalDamageFixedResist = reader.ReadShort();
		PushDamageFixedResist = reader.ReadShort();
		DodgePALostProbability = reader.ReadShort();
		DodgePMLostProbability = reader.ReadShort();
		TackleBlock = reader.ReadShort();
		TackleEvade = reader.ReadShort();
		InvisibilityState = reader.ReadSByte();
	}
}
