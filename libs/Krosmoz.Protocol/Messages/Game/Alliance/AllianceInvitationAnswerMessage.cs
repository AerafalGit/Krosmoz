// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Alliance;

public sealed class AllianceInvitationAnswerMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6401;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static AllianceInvitationAnswerMessage Empty =>
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
