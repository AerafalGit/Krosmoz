// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Game.Guild;

namespace Krosmoz.Protocol.Messages.Game.Alliance;

public sealed class AllianceModificationEmblemValidMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6447;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static AllianceModificationEmblemValidMessage Empty =>
		new() { Alliancemblem = GuildEmblem.Empty };

	public required GuildEmblem Alliancemblem { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		Alliancemblem.Serialize(writer);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		Alliancemblem = GuildEmblem.Empty;
		Alliancemblem.Deserialize(reader);
	}
}
