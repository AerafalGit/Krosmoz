// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Guild;

public sealed class GuildMemberOnlineStatusMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6061;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static GuildMemberOnlineStatusMessage Empty =>
		new() { MemberId = 0, Online = false };

	public required uint MemberId { get; set; }

	public required bool Online { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteVarUInt32(MemberId);
		writer.WriteBoolean(Online);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		MemberId = reader.ReadVarUInt32();
		Online = reader.ReadBoolean();
	}
}
