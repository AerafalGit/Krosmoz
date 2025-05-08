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

	public required int Model { get; set; }

	public required IEnumerable<int> Ancestor { get; set; }

	public required IEnumerable<int> Behaviors { get; set; }

	public required string Name { get; set; }

	public required int OwnerId { get; set; }

	public required double Experience { get; set; }

	public required double ExperienceForLevel { get; set; }

	public required double ExperienceForNextLevel { get; set; }

	public required sbyte Level { get; set; }

	public required int MaxPods { get; set; }

	public required int Stamina { get; set; }

	public required int StaminaMax { get; set; }

	public required int Maturity { get; set; }

	public required int MaturityForAdult { get; set; }

	public required int Energy { get; set; }

	public required int EnergyMax { get; set; }

	public required int Serenity { get; set; }

	public required int AggressivityMax { get; set; }

	public required int SerenityMax { get; set; }

	public required int Love { get; set; }

	public required int LoveMax { get; set; }

	public required int FecondationTime { get; set; }

	public required int BoostLimiter { get; set; }

	public required double BoostMax { get; set; }

	public required int ReproductionCount { get; set; }

	public required int ReproductionCountMax { get; set; }

	public required IEnumerable<ObjectEffectInteger> EffectList { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		var flag = new byte();
		BooleanByteWrapper.SetFlag(flag, 0, Sex);
		BooleanByteWrapper.SetFlag(flag, 1, IsRideable);
		BooleanByteWrapper.SetFlag(flag, 2, IsWild);
		BooleanByteWrapper.SetFlag(flag, 3, IsFecondationReady);
		writer.WriteByte(flag);
		writer.WriteDouble(Id);
		writer.WriteInt(Model);
		var ancestorBefore = writer.Position;
		var ancestorCount = 0;
		writer.WriteShort(0);
		foreach (var item in Ancestor)
		{
			writer.WriteInt(item);
			ancestorCount++;
		}
		var ancestorAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, ancestorBefore);
		writer.WriteShort((short)ancestorCount);
		writer.Seek(SeekOrigin.Begin, ancestorAfter);
		var behaviorsBefore = writer.Position;
		var behaviorsCount = 0;
		writer.WriteShort(0);
		foreach (var item in Behaviors)
		{
			writer.WriteInt(item);
			behaviorsCount++;
		}
		var behaviorsAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, behaviorsBefore);
		writer.WriteShort((short)behaviorsCount);
		writer.Seek(SeekOrigin.Begin, behaviorsAfter);
		writer.WriteUtfLengthPrefixed16(Name);
		writer.WriteInt(OwnerId);
		writer.WriteDouble(Experience);
		writer.WriteDouble(ExperienceForLevel);
		writer.WriteDouble(ExperienceForNextLevel);
		writer.WriteSByte(Level);
		writer.WriteInt(MaxPods);
		writer.WriteInt(Stamina);
		writer.WriteInt(StaminaMax);
		writer.WriteInt(Maturity);
		writer.WriteInt(MaturityForAdult);
		writer.WriteInt(Energy);
		writer.WriteInt(EnergyMax);
		writer.WriteInt(Serenity);
		writer.WriteInt(AggressivityMax);
		writer.WriteInt(SerenityMax);
		writer.WriteInt(Love);
		writer.WriteInt(LoveMax);
		writer.WriteInt(FecondationTime);
		writer.WriteInt(BoostLimiter);
		writer.WriteDouble(BoostMax);
		writer.WriteInt(ReproductionCount);
		writer.WriteInt(ReproductionCountMax);
		var effectListBefore = writer.Position;
		var effectListCount = 0;
		writer.WriteShort(0);
		foreach (var item in EffectList)
		{
			item.Serialize(writer);
			effectListCount++;
		}
		var effectListAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, effectListBefore);
		writer.WriteShort((short)effectListCount);
		writer.Seek(SeekOrigin.Begin, effectListAfter);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		var flag = reader.ReadByte();
		Sex = BooleanByteWrapper.GetFlag(flag, 0);
		IsRideable = BooleanByteWrapper.GetFlag(flag, 1);
		IsWild = BooleanByteWrapper.GetFlag(flag, 2);
		IsFecondationReady = BooleanByteWrapper.GetFlag(flag, 3);
		Id = reader.ReadDouble();
		Model = reader.ReadInt();
		var ancestorCount = reader.ReadShort();
		var ancestor = new int[ancestorCount];
		for (var i = 0; i < ancestorCount; i++)
		{
			ancestor[i] = reader.ReadInt();
		}
		Ancestor = ancestor;
		var behaviorsCount = reader.ReadShort();
		var behaviors = new int[behaviorsCount];
		for (var i = 0; i < behaviorsCount; i++)
		{
			behaviors[i] = reader.ReadInt();
		}
		Behaviors = behaviors;
		Name = reader.ReadUtfLengthPrefixed16();
		OwnerId = reader.ReadInt();
		Experience = reader.ReadDouble();
		ExperienceForLevel = reader.ReadDouble();
		ExperienceForNextLevel = reader.ReadDouble();
		Level = reader.ReadSByte();
		MaxPods = reader.ReadInt();
		Stamina = reader.ReadInt();
		StaminaMax = reader.ReadInt();
		Maturity = reader.ReadInt();
		MaturityForAdult = reader.ReadInt();
		Energy = reader.ReadInt();
		EnergyMax = reader.ReadInt();
		Serenity = reader.ReadInt();
		AggressivityMax = reader.ReadInt();
		SerenityMax = reader.ReadInt();
		Love = reader.ReadInt();
		LoveMax = reader.ReadInt();
		FecondationTime = reader.ReadInt();
		BoostLimiter = reader.ReadInt();
		BoostMax = reader.ReadDouble();
		ReproductionCount = reader.ReadInt();
		ReproductionCountMax = reader.ReadInt();
		var effectListCount = reader.ReadShort();
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
