// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Alliance;

public sealed class AllianceInvitationStateRecrutedMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6392;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static AllianceInvitationStateRecrutedMessage Empty =>
		new() { InvitationState = 0 };

	public required sbyte InvitationState { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteInt8(InvitationState);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		InvitationState = reader.ReadInt8();
	}
}
