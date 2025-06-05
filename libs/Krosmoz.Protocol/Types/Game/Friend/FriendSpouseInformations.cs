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

	public required uint SpouseId { get; set; }

	public required string SpouseName { get; set; }

	public required byte SpouseLevel { get; set; }

	public required sbyte Breed { get; set; }

	public required sbyte Sex { get; set; }

	public required EntityLook SpouseEntityLook { get; set; }

	public required BasicGuildInformations GuildInfo { get; set; }

	public required sbyte AlignmentSide { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteInt32(SpouseAccountId);
		writer.WriteVarUInt32(SpouseId);
		writer.WriteUtfPrefixedLength16(SpouseName);
		writer.WriteUInt8(SpouseLevel);
		writer.WriteInt8(Breed);
		writer.WriteInt8(Sex);
		SpouseEntityLook.Serialize(writer);
		GuildInfo.Serialize(writer);
		writer.WriteInt8(AlignmentSide);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		SpouseAccountId = reader.ReadInt32();
		SpouseId = reader.ReadVarUInt32();
		SpouseName = reader.ReadUtfPrefixedLength16();
		SpouseLevel = reader.ReadUInt8();
		Breed = reader.ReadInt8();
		Sex = reader.ReadInt8();
		SpouseEntityLook = EntityLook.Empty;
		SpouseEntityLook.Deserialize(reader);
		GuildInfo = BasicGuildInformations.Empty;
		GuildInfo.Deserialize(reader);
		AlignmentSide = reader.ReadInt8();
	}
}
