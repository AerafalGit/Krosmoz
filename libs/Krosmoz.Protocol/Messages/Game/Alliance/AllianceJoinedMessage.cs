// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Game.Context.Roleplay;

namespace Krosmoz.Protocol.Messages.Game.Alliance;

public class AllianceJoinedMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6402;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static AllianceJoinedMessage Empty =>
		new() { AllianceInfo = AllianceInformations.Empty, Enabled = false };

	public required AllianceInformations AllianceInfo { get; set; }

	public required bool Enabled { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		AllianceInfo.Serialize(writer);
		writer.WriteBoolean(Enabled);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		AllianceInfo = AllianceInformations.Empty;
		AllianceInfo.Deserialize(reader);
		Enabled = reader.ReadBoolean();
	}
}
