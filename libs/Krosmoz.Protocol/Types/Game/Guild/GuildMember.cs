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

	public required short Rank { get; set; }

	public required double GivenExperience { get; set; }

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
		writer.WriteSByte(Breed);
		writer.WriteBoolean(Sex);
		writer.WriteShort(Rank);
		writer.WriteDouble(GivenExperience);
		writer.WriteSByte(ExperienceGivenPercent);
		writer.WriteUInt(Rights);
		writer.WriteSByte(Connected);
		writer.WriteSByte(AlignmentSide);
		writer.WriteUShort(HoursSinceLastConnection);
		writer.WriteSByte(MoodSmileyId);
		writer.WriteInt(AccountId);
		writer.WriteInt(AchievementPoints);
		writer.WriteUShort(Status.ProtocolId);
		Status.Serialize(writer);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		Breed = reader.ReadSByte();
		Sex = reader.ReadBoolean();
		Rank = reader.ReadShort();
		GivenExperience = reader.ReadDouble();
		ExperienceGivenPercent = reader.ReadSByte();
		Rights = reader.ReadUInt();
		Connected = reader.ReadSByte();
		AlignmentSide = reader.ReadSByte();
		HoursSinceLastConnection = reader.ReadUShort();
		MoodSmileyId = reader.ReadSByte();
		AccountId = reader.ReadInt();
		AchievementPoints = reader.ReadInt();
		Status = Types.TypeFactory.CreateType<PlayerStatus>(reader.ReadUShort());
		Status.Deserialize(reader);
	}
}
