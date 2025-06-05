// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Game.Data.Items.Effects;

namespace Krosmoz.Protocol.Types.Game.Mount;

public sealed class MountClientData : DofusType
{
	public new const ushort StaticProtocolId = 178;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static MountClientData Empty =>
		new() { Sex = false, IsRideable = false, IsWild = false, IsFecondationReady = false, Id = 0, Model = 0, Ancestor = [], Behaviors = [], Name = string.Empty, OwnerId = 0, Experience = 0, ExperienceForLevel = 0, ExperienceForNextLevel = 0, Level = 0, MaxPods = 0, Stamina = 0, StaminaMax = 0, Maturity = 0, MaturityForAdult = 0, Energy = 0, EnergyMax = 0, Serenity = 0, AggressivityMax = 0, SerenityMax = 0, Love = 0, LoveMax = 0, FecondationTime = 0, BoostLimiter = 0, BoostMax = 0, ReproductionCount = 0, ReproductionCountMax = 0, EffectList = [] };

	public required bool Sex { get; set; }

	public required bool IsRideable { get; set; }

	public required bool IsWild { get; set; }

	public required bool IsFecondationReady { get; set; }

	public required double Id { get; set; }

	public required uint Model { get; set; }

	public required IEnumerable<int> Ancestor { get; set; }

	public required IEnumerable<int> Behaviors { get; set; }

	public required string Name { get; set; }

	public required int OwnerId { get; set; }

	public required ulong Experience { get; set; }

	public required ulong ExperienceForLevel { get; set; }

	public required double ExperienceForNextLevel { get; set; }

	public required sbyte Level { get; set; }

	public required uint MaxPods { get; set; }

	public required uint Stamina { get; set; }

	public required uint StaminaMax { get; set; }

	public required uint Maturity { get; set; }

	public required uint MaturityForAdult { get; set; }

	public required uint Energy { get; set; }

	public required uint EnergyMax { get; set; }

	public required int Serenity { get; set; }

	public required int AggressivityMax { get; set; }

	public required uint SerenityMax { get; set; }

	public required uint Love { get; set; }

	public required uint LoveMax { get; set; }

	public required int FecondationTime { get; set; }

	public required int BoostLimiter { get; set; }

	public required double BoostMax { get; set; }

	public required int ReproductionCount { get; set; }

	public required uint ReproductionCountMax { get; set; }

	public required IEnumerable<ObjectEffectInteger> EffectList { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		var flag = new byte();
		flag = BooleanByteWrapper.SetFlag(flag, 0, Sex);
		flag = BooleanByteWrapper.SetFlag(flag, 1, IsRideable);
		flag = BooleanByteWrapper.SetFlag(flag, 2, IsWild);
		flag = BooleanByteWrapper.SetFlag(flag, 3, IsFecondationReady);
		writer.WriteUInt8(flag);
		writer.WriteDouble(Id);
		writer.WriteVarUInt32(Model);
		var ancestorBefore = writer.Position;
		var ancestorCount = 0;
		writer.WriteInt16(0);
		foreach (var item in Ancestor)
		{
			writer.WriteInt32(item);
			ancestorCount++;
		}
		var ancestorAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, ancestorBefore);
		writer.WriteInt16((short)ancestorCount);
		writer.Seek(SeekOrigin.Begin, ancestorAfter);
		var behaviorsBefore = writer.Position;
		var behaviorsCount = 0;
		writer.WriteInt16(0);
		foreach (var item in Behaviors)
		{
			writer.WriteInt32(item);
			behaviorsCount++;
		}
		var behaviorsAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, behaviorsBefore);
		writer.WriteInt16((short)behaviorsCount);
		writer.Seek(SeekOrigin.Begin, behaviorsAfter);
		writer.WriteUtfPrefixedLength16(Name);
		writer.WriteInt32(OwnerId);
		writer.WriteVarUInt64(Experience);
		writer.WriteVarUInt64(ExperienceForLevel);
		writer.WriteDouble(ExperienceForNextLevel);
		writer.WriteInt8(Level);
		writer.WriteVarUInt32(MaxPods);
		writer.WriteVarUInt32(Stamina);
		writer.WriteVarUInt32(StaminaMax);
		writer.WriteVarUInt32(Maturity);
		writer.WriteVarUInt32(MaturityForAdult);
		writer.WriteVarUInt32(Energy);
		writer.WriteVarUInt32(EnergyMax);
		writer.WriteInt32(Serenity);
		writer.WriteInt32(AggressivityMax);
		writer.WriteVarUInt32(SerenityMax);
		writer.WriteVarUInt32(Love);
		writer.WriteVarUInt32(LoveMax);
		writer.WriteInt32(FecondationTime);
		writer.WriteInt32(BoostLimiter);
		writer.WriteDouble(BoostMax);
		writer.WriteInt32(ReproductionCount);
		writer.WriteVarUInt32(ReproductionCountMax);
		var effectListBefore = writer.Position;
		var effectListCount = 0;
		writer.WriteInt16(0);
		foreach (var item in EffectList)
		{
			item.Serialize(writer);
			effectListCount++;
		}
		var effectListAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, effectListBefore);
		writer.WriteInt16((short)effectListCount);
		writer.Seek(SeekOrigin.Begin, effectListAfter);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		var flag = reader.ReadUInt8();
		Sex = BooleanByteWrapper.GetFlag(flag, 0);
		IsRideable = BooleanByteWrapper.GetFlag(flag, 1);
		IsWild = BooleanByteWrapper.GetFlag(flag, 2);
		IsFecondationReady = BooleanByteWrapper.GetFlag(flag, 3);
		Id = reader.ReadDouble();
		Model = reader.ReadVarUInt32();
		var ancestorCount = reader.ReadInt16();
		var ancestor = new int[ancestorCount];
		for (var i = 0; i < ancestorCount; i++)
		{
			ancestor[i] = reader.ReadInt32();
		}
		Ancestor = ancestor;
		var behaviorsCount = reader.ReadInt16();
		var behaviors = new int[behaviorsCount];
		for (var i = 0; i < behaviorsCount; i++)
		{
			behaviors[i] = reader.ReadInt32();
		}
		Behaviors = behaviors;
		Name = reader.ReadUtfPrefixedLength16();
		OwnerId = reader.ReadInt32();
		Experience = reader.ReadVarUInt64();
		ExperienceForLevel = reader.ReadVarUInt64();
		ExperienceForNextLevel = reader.ReadDouble();
		Level = reader.ReadInt8();
		MaxPods = reader.ReadVarUInt32();
		Stamina = reader.ReadVarUInt32();
		StaminaMax = reader.ReadVarUInt32();
		Maturity = reader.ReadVarUInt32();
		MaturityForAdult = reader.ReadVarUInt32();
		Energy = reader.ReadVarUInt32();
		EnergyMax = reader.ReadVarUInt32();
		Serenity = reader.ReadInt32();
		AggressivityMax = reader.ReadInt32();
		SerenityMax = reader.ReadVarUInt32();
		Love = reader.ReadVarUInt32();
		LoveMax = reader.ReadVarUInt32();
		FecondationTime = reader.ReadInt32();
		BoostLimiter = reader.ReadInt32();
		BoostMax = reader.ReadDouble();
		ReproductionCount = reader.ReadInt32();
		ReproductionCountMax = reader.ReadVarUInt32();
		var effectListCount = reader.ReadInt16();
		var effectList = new ObjectEffectInteger[effectListCount];
		for (var i = 0; i < effectListCount; i++)
		{
			var entry = ObjectEffectInteger.Empty;
			entry.Deserialize(reader);
			effectList[i] = entry;
		}
		EffectList = effectList;
	}
}
