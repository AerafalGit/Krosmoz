// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Types.Game.Friend;

public class FriendInformations : AbstractContactInformations
{
	public new const ushort StaticProtocolId = 78;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static new FriendInformations Empty =>
		new() { AccountName = string.Empty, AccountId = 0, PlayerState = 0, LastConnection = 0, AchievementPoints = 0 };

	public required sbyte PlayerState { get; set; }

	public required int LastConnection { get; set; }

	public required int AchievementPoints { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		writer.WriteSByte(PlayerState);
		writer.WriteInt(LastConnection);
		writer.WriteInt(AchievementPoints);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		PlayerState = reader.ReadSByte();
		LastConnection = reader.ReadInt();
		AchievementPoints = reader.ReadInt();
	}
}
