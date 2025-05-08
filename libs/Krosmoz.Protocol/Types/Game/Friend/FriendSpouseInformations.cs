// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Game.Context.Roleplay;
using Krosmoz.Protocol.Types.Game.Look;

namespace Krosmoz.Protocol.Types.Game.Friend;

public class FriendSpouseInformations : DofusType
{
	public new const ushort StaticProtocolId = 77;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static FriendSpouseInformations Empty =>
		new() { SpouseAccountId = 0, SpouseId = 0, SpouseName = string.Empty, SpouseLevel = 0, Breed = 0, Sex = 0, SpouseEntityLook = EntityLook.Empty, GuildInfo = BasicGuildInformations.Empty, AlignmentSide = 0 };

	public required int SpouseAccountId { get; set; }

	public required int SpouseId { get; set; }

	public required string SpouseName { get; set; }

	public required byte SpouseLevel { get; set; }

	public required sbyte Breed { get; set; }

	public required sbyte Sex { get; set; }

	public required EntityLook SpouseEntityLook { get; set; }

	public required BasicGuildInformations GuildInfo { get; set; }

	public required sbyte AlignmentSide { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteInt(SpouseAccountId);
		writer.WriteInt(SpouseId);
		writer.WriteUtfLengthPrefixed16(SpouseName);
		writer.WriteByte(SpouseLevel);
		writer.WriteSByte(Breed);
		writer.WriteSByte(Sex);
		SpouseEntityLook.Serialize(writer);
		GuildInfo.Serialize(writer);
		writer.WriteSByte(AlignmentSide);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		SpouseAccountId = reader.ReadInt();
		SpouseId = reader.ReadInt();
		SpouseName = reader.ReadUtfLengthPrefixed16();
		SpouseLevel = reader.ReadByte();
		Breed = reader.ReadSByte();
		Sex = reader.ReadSByte();
		SpouseEntityLook = EntityLook.Empty;
		SpouseEntityLook.Deserialize(reader);
		GuildInfo = BasicGuildInformations.Empty;
		GuildInfo.Deserialize(reader);
		AlignmentSide = reader.ReadSByte();
	}
}
