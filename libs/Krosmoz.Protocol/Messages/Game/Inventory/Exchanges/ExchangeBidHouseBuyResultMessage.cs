// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Inventory.Exchanges;

public sealed class ExchangeBidHouseBuyResultMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6272;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static ExchangeBidHouseBuyResultMessage Empty =>
		new() { Uid = 0, Bought = false };

	public required uint Uid { get; set; }

	public required bool Bought { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteVarUInt32(Uid);
		writer.WriteBoolean(Bought);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		Uid = reader.ReadVarUInt32();
		Bought = reader.ReadBoolean();
	}
}
