// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Game.Character.Status;
using Krosmoz.Protocol.Types.Game.Context.Roleplay;

namespace Krosmoz.Protocol.Types.Game.Friend;

public sealed class FriendOnlineInformations : FriendInformations
{
	public new const ushort StaticProtocolId = 92;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static new FriendOnlineInformations Empty =>
		new() { AccountName = string.Empty, AccountId = 0, AchievementPoints = 0, LastConnection = 0, PlayerState = 0, PlayerId = 0, PlayerName = string.Empty, Level = 0, AlignmentSide = 0, Breed = 0, Sex = false, GuildInfo = BasicGuildInformations.Empty, MoodSmileyId = 0, Status = PlayerStatus.Empty };

	public required uint PlayerId { get; set; }

	public required string PlayerName { get; set; }

	public required byte Level { get; set; }

	public required sbyte AlignmentSide { get; set; }

	public required sbyte Breed { get; set; }

	public required bool Sex { get; set; }

	public required BasicGuildInformations GuildInfo { get; set; }

	public required sbyte MoodSmileyId { get; set; }

	public required PlayerStatus Status { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		writer.WriteVarUInt32(PlayerId);
		writer.WriteUtfPrefixedLength16(PlayerName);
		writer.WriteUInt8(Level);
		writer.WriteInt8(AlignmentSide);
		writer.WriteInt8(Breed);
		writer.WriteBoolean(Sex);
		GuildInfo.Serialize(writer);
		writer.WriteInt8(MoodSmileyId);
		writer.WriteUInt16(Status.ProtocolId);
		Status.Serialize(writer);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		PlayerId = reader.ReadVarUInt32();
		PlayerName = reader.ReadUtfPrefixedLength16();
		Level = reader.ReadUInt8();
		AlignmentSide = reader.ReadInt8();
		Breed = reader.ReadInt8();
		Sex = reader.ReadBoolean();
		GuildInfo = BasicGuildInformations.Empty;
		GuildInfo.Deserialize(reader);
		MoodSmileyId = reader.ReadInt8();
		Status = Types.TypeFactory.CreateType<PlayerStatus>(reader.ReadUInt16());
		Status.Deserialize(reader);
	}
}
