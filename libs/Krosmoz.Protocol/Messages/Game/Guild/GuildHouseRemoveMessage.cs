// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Guild;

public sealed class GuildHouseRemoveMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6180;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static GuildHouseRemoveMessage Empty =>
		new() { HouseId = 0 };

	public required uint HouseId { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteVarUInt32(HouseId);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		HouseId = reader.ReadVarUInt32();
	}
}
