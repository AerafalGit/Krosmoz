// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Alliance;

public sealed class AllianceInvitationMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6395;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static AllianceInvitationMessage Empty =>
		new() { TargetId = 0 };

	public required uint TargetId { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteVarUInt32(TargetId);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		TargetId = reader.ReadVarUInt32();
	}
}
