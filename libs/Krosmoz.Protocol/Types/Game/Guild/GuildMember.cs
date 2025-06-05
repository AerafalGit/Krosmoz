// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Game.Character;
using Krosmoz.Protocol.Types.Game.Character.Status;

namespace Krosmoz.Protocol.Types.Game.Guild;

public sealed class GuildMember : CharacterMinimalInformations
{
	public new const ushort StaticProtocolId = 88;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static new GuildMember Empty =>
		new() { Id = 0, Name = string.Empty, Level = 0, Breed = 0, Sex = false, Rank = 0, GivenExperience = 0, ExperienceGivenPercent = 0, Rights = 0, Connected = 0, AlignmentSide = 0, HoursSinceLastConnection = 0, MoodSmileyId = 0, AccountId = 0, AchievementPoints = 0, Status = PlayerStatus.Empty };

	public required sbyte Breed { get; set; }

	public required bool Sex { get; set; }

	public required ushort Rank { get; set; }

	public required ulong GivenExperience { get; set; }

	public required sbyte ExperienceGivenPercent { get; set; }

	public required uint Rights { get; set; }

	public required sbyte Connected { get; set; }

	public required sbyte AlignmentSide { get; set; }

	public required ushort HoursSinceLastConnection { get; set; }

	public required sbyte MoodSmileyId { get; set; }

	public required int AccountId { get; set; }

	public required int AchievementPoints { get; set; }

	public required PlayerStatus Status { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		writer.WriteInt8(Breed);
		writer.WriteBoolean(Sex);
		writer.WriteVarUInt16(Rank);
		writer.WriteVarUInt64(GivenExperience);
		writer.WriteInt8(ExperienceGivenPercent);
		writer.WriteVarUInt32(Rights);
		writer.WriteInt8(Connected);
		writer.WriteInt8(AlignmentSide);
		writer.WriteUInt16(HoursSinceLastConnection);
		writer.WriteInt8(MoodSmileyId);
		writer.WriteInt32(AccountId);
		writer.WriteInt32(AchievementPoints);
		writer.WriteUInt16(Status.ProtocolId);
		Status.Serialize(writer);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		Breed = reader.ReadInt8();
		Sex = reader.ReadBoolean();
		Rank = reader.ReadVarUInt16();
		GivenExperience = reader.ReadVarUInt64();
		ExperienceGivenPercent = reader.ReadInt8();
		Rights = reader.ReadVarUInt32();
		Connected = reader.ReadInt8();
		AlignmentSide = reader.ReadInt8();
		HoursSinceLastConnection = reader.ReadUInt16();
		MoodSmileyId = reader.ReadInt8();
		AccountId = reader.ReadInt32();
		AchievementPoints = reader.ReadInt32();
		Status = Types.TypeFactory.CreateType<PlayerStatus>(reader.ReadUInt16());
		Status.Deserialize(reader);
	}
}
