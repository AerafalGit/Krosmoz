// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Game.Context.Roleplay;

namespace Krosmoz.Protocol.Messages.Game.Guild;

public sealed class GuildInvitedMessage : DofusMessage
{
	public new const uint StaticProtocolId = 5552;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static GuildInvitedMessage Empty =>
		new() { RecruterId = 0, RecruterName = string.Empty, GuildInfo = BasicGuildInformations.Empty };

	public required int RecruterId { get; set; }

	public required string RecruterName { get; set; }

	public required BasicGuildInformations GuildInfo { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteInt(RecruterId);
		writer.WriteUtfLengthPrefixed16(RecruterName);
		GuildInfo.Serialize(writer);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		RecruterId = reader.ReadInt();
		RecruterName = reader.ReadUtfLengthPrefixed16();
		GuildInfo = BasicGuildInformations.Empty;
		GuildInfo.Deserialize(reader);
	}
}
