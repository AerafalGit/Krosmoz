// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Game.Context.Roleplay;

namespace Krosmoz.Protocol.Messages.Game.Alliance;

public sealed class AllianceInvitedMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6397;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static AllianceInvitedMessage Empty =>
		new() { RecruterId = 0, RecruterName = string.Empty, AllianceInfo = BasicNamedAllianceInformations.Empty };

	public required uint RecruterId { get; set; }

	public required string RecruterName { get; set; }

	public required BasicNamedAllianceInformations AllianceInfo { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteVarUInt32(RecruterId);
		writer.WriteUtfPrefixedLength16(RecruterName);
		AllianceInfo.Serialize(writer);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		RecruterId = reader.ReadVarUInt32();
		RecruterName = reader.ReadUtfPrefixedLength16();
		AllianceInfo = BasicNamedAllianceInformations.Empty;
		AllianceInfo.Deserialize(reader);
	}
}
