// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Inventory.Exchanges;

public class ExchangeRequestMessage : DofusMessage
{
	public new const uint StaticProtocolId = 5505;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static ExchangeRequestMessage Empty =>
		new() { ExchangeType = 0 };

	public required sbyte ExchangeType { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteInt8(ExchangeType);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		ExchangeType = reader.ReadInt8();
	}
}
