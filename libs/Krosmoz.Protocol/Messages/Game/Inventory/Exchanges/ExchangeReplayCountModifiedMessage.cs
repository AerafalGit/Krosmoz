// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Inventory.Exchanges;

public sealed class ExchangeReplayCountModifiedMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6023;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static ExchangeReplayCountModifiedMessage Empty =>
		new() { Count = 0 };

	public required int Count { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteVarInt32(Count);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		Count = reader.ReadVarInt32();
	}
}
