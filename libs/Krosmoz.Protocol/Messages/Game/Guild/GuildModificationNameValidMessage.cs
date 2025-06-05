// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Guild;

public sealed class GuildModificationNameValidMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6327;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static GuildModificationNameValidMessage Empty =>
		new() { GuildName = string.Empty };

	public required string GuildName { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteUtfPrefixedLength16(GuildName);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		GuildName = reader.ReadUtfPrefixedLength16();
	}
}
