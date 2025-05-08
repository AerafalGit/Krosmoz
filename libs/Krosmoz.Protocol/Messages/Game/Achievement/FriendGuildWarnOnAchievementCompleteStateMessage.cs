// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Achievement;

public sealed class FriendGuildWarnOnAchievementCompleteStateMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6383;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static FriendGuildWarnOnAchievementCompleteStateMessage Empty =>
		new() { Enable = false };

	public required bool Enable { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteBoolean(Enable);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		Enable = reader.ReadBoolean();
	}
}
