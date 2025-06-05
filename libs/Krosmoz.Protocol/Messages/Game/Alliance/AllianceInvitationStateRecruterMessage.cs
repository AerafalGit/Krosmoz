// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Alliance;

public sealed class AllianceInvitationStateRecruterMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6396;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static AllianceInvitationStateRecruterMessage Empty =>
		new() { RecrutedName = string.Empty, InvitationState = 0 };

	public required string RecrutedName { get; set; }

	public required sbyte InvitationState { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteUtfPrefixedLength16(RecrutedName);
		writer.WriteInt8(InvitationState);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		RecrutedName = reader.ReadUtfPrefixedLength16();
		InvitationState = reader.ReadInt8();
	}
}
