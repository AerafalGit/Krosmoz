// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Alliance;

public sealed class AllianceGuildLeavingMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6399;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static AllianceGuildLeavingMessage Empty =>
		new() { Kicked = false, GuildId = 0 };

	public required bool Kicked { get; set; }

	public required uint GuildId { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteBoolean(Kicked);
		writer.WriteVarUInt32(GuildId);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		Kicked = reader.ReadBoolean();
		GuildId = reader.ReadVarUInt32();
	}
}
