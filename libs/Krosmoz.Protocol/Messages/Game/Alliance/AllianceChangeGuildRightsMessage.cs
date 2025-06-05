// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Alliance;

public sealed class AllianceChangeGuildRightsMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6426;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static AllianceChangeGuildRightsMessage Empty =>
		new() { GuildId = 0, Rights = 0 };

	public required uint GuildId { get; set; }

	public required sbyte Rights { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteVarUInt32(GuildId);
		writer.WriteInt8(Rights);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		GuildId = reader.ReadVarUInt32();
		Rights = reader.ReadInt8();
	}
}
