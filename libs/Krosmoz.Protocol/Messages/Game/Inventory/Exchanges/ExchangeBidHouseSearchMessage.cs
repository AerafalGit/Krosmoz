// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Inventory.Exchanges;

public sealed class ExchangeBidHouseSearchMessage : DofusMessage
{
	public new const uint StaticProtocolId = 5806;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static ExchangeBidHouseSearchMessage Empty =>
		new() { Type = 0, GenId = 0 };

	public required int Type { get; set; }

	public required int GenId { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteInt(Type);
		writer.WriteInt(GenId);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		Type = reader.ReadInt();
		GenId = reader.ReadInt();
	}
}
