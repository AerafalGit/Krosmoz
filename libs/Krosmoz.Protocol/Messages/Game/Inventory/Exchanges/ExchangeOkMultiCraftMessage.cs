// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Inventory.Exchanges;

public sealed class ExchangeOkMultiCraftMessage : DofusMessage
{
	public new const uint StaticProtocolId = 5768;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static ExchangeOkMultiCraftMessage Empty =>
		new() { InitiatorId = 0, OtherId = 0, Role = 0 };

	public required uint InitiatorId { get; set; }

	public required uint OtherId { get; set; }

	public required sbyte Role { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteVarUInt32(InitiatorId);
		writer.WriteVarUInt32(OtherId);
		writer.WriteInt8(Role);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		InitiatorId = reader.ReadVarUInt32();
		OtherId = reader.ReadVarUInt32();
		Role = reader.ReadInt8();
	}
}
