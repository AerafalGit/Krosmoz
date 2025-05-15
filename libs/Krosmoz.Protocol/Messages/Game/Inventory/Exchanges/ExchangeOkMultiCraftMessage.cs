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

	public required int InitiatorId { get; set; }

	public required int OtherId { get; set; }

	public required sbyte Role { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteInt32(InitiatorId);
		writer.WriteInt32(OtherId);
		writer.WriteInt8(Role);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		InitiatorId = reader.ReadInt32();
		OtherId = reader.ReadInt32();
		Role = reader.ReadInt8();
	}
}
