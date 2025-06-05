// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Game.Context.Roleplay;
using Krosmoz.Protocol.Types.Game.Look;

namespace Krosmoz.Protocol.Types.Game.Friend;

public sealed class FriendSpouseOnlineInformations : FriendSpouseInformations
{
	public new const ushort StaticProtocolId = 93;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static new FriendSpouseOnlineInformations Empty =>
		new() { AlignmentSide = 0, GuildInfo = BasicGuildInformations.Empty, SpouseEntityLook = EntityLook.Empty, Sex = 0, Breed = 0, SpouseLevel = 0, SpouseName = string.Empty, SpouseId = 0, SpouseAccountId = 0, InFight = false, FollowSpouse = false, MapId = 0, SubAreaId = 0 };

	public required bool InFight { get; set; }

	public required bool FollowSpouse { get; set; }

	public required int MapId { get; set; }

	public required ushort SubAreaId { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		var flag = new byte();
		flag = BooleanByteWrapper.SetFlag(flag, 0, InFight);
		flag = BooleanByteWrapper.SetFlag(flag, 1, FollowSpouse);
		writer.WriteUInt8(flag);
		writer.WriteInt32(MapId);
		writer.WriteVarUInt16(SubAreaId);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		var flag = reader.ReadUInt8();
		InFight = BooleanByteWrapper.GetFlag(flag, 0);
		FollowSpouse = BooleanByteWrapper.GetFlag(flag, 1);
		MapId = reader.ReadInt32();
		SubAreaId = reader.ReadVarUInt16();
	}
}
