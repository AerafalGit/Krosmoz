// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Guild;

public sealed class GuildInvitationAnswerMessage : DofusMessage
{
	public new const uint StaticProtocolId = 5556;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static GuildInvitationAnswerMessage Empty =>
		new() { Accept = false };

	public required bool Accept { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteBoolean(Accept);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		Accept = reader.ReadBoolean();
	}
}
