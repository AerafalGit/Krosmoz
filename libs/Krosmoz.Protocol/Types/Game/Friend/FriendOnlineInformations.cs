// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Game.Context.Roleplay;
using PlayerStatus = Krosmoz.Protocol.Types.Game.Character.Status.PlayerStatus;

namespace Krosmoz.Protocol.Types.Game.Friend;

public sealed class FriendOnlineInformations : FriendInformations
{
	public new const ushort StaticProtocolId = 92;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static new FriendOnlineInformations Empty =>
		new() { AccountName = string.Empty, AccountId = 0, AchievementPoints = 0, LastConnection = 0, PlayerState = 0, PlayerId = 0, PlayerName = string.Empty, Level = 0, AlignmentSide = 0, Breed = 0, Sex = false, GuildInfo = BasicGuildInformations.Empty, MoodSmileyId = 0, Status = PlayerStatus.Empty };

	public required int PlayerId { get; set; }

	public required string PlayerName { get; set; }

	public required short Level { get; set; }

	public required sbyte AlignmentSide { get; set; }

	public required sbyte Breed { get; set; }

	public required bool Sex { get; set; }

	public required BasicGuildInformations GuildInfo { get; set; }

	public required sbyte MoodSmileyId { get; set; }

	public required PlayerStatus Status { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		writer.WriteInt(PlayerId);
		writer.WriteUtfLengthPrefixed16(PlayerName);
		writer.WriteShort(Level);
		writer.WriteSByte(AlignmentSide);
		writer.WriteSByte(Breed);
		writer.WriteBoolean(Sex);
		GuildInfo.Serialize(writer);
		writer.WriteSByte(MoodSmileyId);
		writer.WriteUShort(Status.ProtocolId);
		Status.Serialize(writer);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		PlayerId = reader.ReadInt();
		PlayerName = reader.ReadUtfLengthPrefixed16();
		Level = reader.ReadShort();
		AlignmentSide = reader.ReadSByte();
		Breed = reader.ReadSByte();
		Sex = reader.ReadBoolean();
		GuildInfo = BasicGuildInformations.Empty;
		GuildInfo.Deserialize(reader);
		MoodSmileyId = reader.ReadSByte();
		Status = Types.TypeFactory.CreateType<PlayerStatus>(reader.ReadUShort());
		Status.Deserialize(reader);
	}
}
