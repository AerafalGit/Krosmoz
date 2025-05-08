// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Game.Context.Roleplay;

namespace Krosmoz.Protocol.Messages.Game.Context.Roleplay.Houses.Guild;

public sealed class HouseGuildRightsMessage : DofusMessage
{
	public new const uint StaticProtocolId = 5703;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static HouseGuildRightsMessage Empty =>
		new() { HouseId = 0, GuildInfo = GuildInformations.Empty, Rights = 0 };

	public required short HouseId { get; set; }

	public required GuildInformations GuildInfo { get; set; }

	public required uint Rights { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteShort(HouseId);
		GuildInfo.Serialize(writer);
		writer.WriteUInt(Rights);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		HouseId = reader.ReadShort();
		GuildInfo = GuildInformations.Empty;
		GuildInfo.Deserialize(reader);
		Rights = reader.ReadUInt();
	}
}
