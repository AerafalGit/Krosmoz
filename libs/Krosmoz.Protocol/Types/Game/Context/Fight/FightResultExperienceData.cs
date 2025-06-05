// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Types.Game.Context.Fight;

public sealed class FightResultExperienceData : FightResultAdditionalData
{
	public new const ushort StaticProtocolId = 192;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static new FightResultExperienceData Empty =>
		new() { ShowExperience = false, ShowExperienceLevelFloor = false, ShowExperienceNextLevelFloor = false, ShowExperienceFightDelta = false, ShowExperienceForGuild = false, ShowExperienceForMount = false, IsIncarnationExperience = false, Experience = 0, ExperienceLevelFloor = 0, ExperienceNextLevelFloor = 0, ExperienceFightDelta = 0, ExperienceForGuild = 0, ExperienceForMount = 0, RerollExperienceMul = 0 };

	public required bool ShowExperience { get; set; }

	public required bool ShowExperienceLevelFloor { get; set; }

	public required bool ShowExperienceNextLevelFloor { get; set; }

	public required bool ShowExperienceFightDelta { get; set; }

	public required bool ShowExperienceForGuild { get; set; }

	public required bool ShowExperienceForMount { get; set; }

	public required bool IsIncarnationExperience { get; set; }

	public required ulong Experience { get; set; }

	public required ulong ExperienceLevelFloor { get; set; }

	public required double ExperienceNextLevelFloor { get; set; }

	public required int ExperienceFightDelta { get; set; }

	public required uint ExperienceForGuild { get; set; }

	public required uint ExperienceForMount { get; set; }

	public required sbyte RerollExperienceMul { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		var flag = new byte();
		flag = BooleanByteWrapper.SetFlag(flag, 0, ShowExperience);
		flag = BooleanByteWrapper.SetFlag(flag, 1, ShowExperienceLevelFloor);
		flag = BooleanByteWrapper.SetFlag(flag, 2, ShowExperienceNextLevelFloor);
		flag = BooleanByteWrapper.SetFlag(flag, 3, ShowExperienceFightDelta);
		flag = BooleanByteWrapper.SetFlag(flag, 4, ShowExperienceForGuild);
		flag = BooleanByteWrapper.SetFlag(flag, 5, ShowExperienceForMount);
		flag = BooleanByteWrapper.SetFlag(flag, 6, IsIncarnationExperience);
		writer.WriteUInt8(flag);
		writer.WriteVarUInt64(Experience);
		writer.WriteVarUInt64(ExperienceLevelFloor);
		writer.WriteDouble(ExperienceNextLevelFloor);
		writer.WriteVarInt32(ExperienceFightDelta);
		writer.WriteVarUInt32(ExperienceForGuild);
		writer.WriteVarUInt32(ExperienceForMount);
		writer.WriteInt8(RerollExperienceMul);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		var flag = reader.ReadUInt8();
		ShowExperience = BooleanByteWrapper.GetFlag(flag, 0);
		ShowExperienceLevelFloor = BooleanByteWrapper.GetFlag(flag, 1);
		ShowExperienceNextLevelFloor = BooleanByteWrapper.GetFlag(flag, 2);
		ShowExperienceFightDelta = BooleanByteWrapper.GetFlag(flag, 3);
		ShowExperienceForGuild = BooleanByteWrapper.GetFlag(flag, 4);
		ShowExperienceForMount = BooleanByteWrapper.GetFlag(flag, 5);
		IsIncarnationExperience = BooleanByteWrapper.GetFlag(flag, 6);
		Experience = reader.ReadVarUInt64();
		ExperienceLevelFloor = reader.ReadVarUInt64();
		ExperienceNextLevelFloor = reader.ReadDouble();
		ExperienceFightDelta = reader.ReadVarInt32();
		ExperienceForGuild = reader.ReadVarUInt32();
		ExperienceForMount = reader.ReadVarUInt32();
		RerollExperienceMul = reader.ReadInt8();
	}
}
