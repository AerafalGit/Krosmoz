// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Context.Roleplay.Houses.Guild;

public sealed class HouseGuildNoneMessage : DofusMessage
{
	public new const uint StaticProtocolId = 5701;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static HouseGuildNoneMessage Empty =>
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
