// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Guild;

public sealed class GuildFactsRequestMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6404;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static GuildFactsRequestMessage Empty =>
		new() { GuildId = 0 };

	public required uint GuildId { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteVarUInt32(GuildId);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		GuildId = reader.ReadVarUInt32();
	}
}
