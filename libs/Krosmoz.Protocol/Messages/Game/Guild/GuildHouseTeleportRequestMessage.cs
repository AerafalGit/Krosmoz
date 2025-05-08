// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Guild;

public sealed class GuildHouseTeleportRequestMessage : DofusMessage
{
	public new const uint StaticProtocolId = 5712;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static GuildHouseTeleportRequestMessage Empty =>
		new() { HouseId = 0 };

	public required int HouseId { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteInt(HouseId);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		HouseId = reader.ReadInt();
	}
}
