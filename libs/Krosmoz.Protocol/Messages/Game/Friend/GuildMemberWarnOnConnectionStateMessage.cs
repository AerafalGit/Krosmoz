// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Friend;

public sealed class GuildMemberWarnOnConnectionStateMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6160;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static GuildMemberWarnOnConnectionStateMessage Empty =>
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
