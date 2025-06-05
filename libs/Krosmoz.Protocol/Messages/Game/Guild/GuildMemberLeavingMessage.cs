// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Guild;

public sealed class GuildMemberLeavingMessage : DofusMessage
{
	public new const uint StaticProtocolId = 5923;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static GuildMemberLeavingMessage Empty =>
		new() { Kicked = false, MemberId = 0 };

	public required bool Kicked { get; set; }

	public required int MemberId { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteBoolean(Kicked);
		writer.WriteInt32(MemberId);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		Kicked = reader.ReadBoolean();
		MemberId = reader.ReadInt32();
	}
}
