// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Inventory.Exchanges;

public sealed class ExchangeRequestedTradeMessage : ExchangeRequestedMessage
{
	public new const uint StaticProtocolId = 5523;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static new ExchangeRequestedTradeMessage Empty =>
		new() { ExchangeType = 0, Source = 0, Target = 0 };

	public required int Source { get; set; }

	public required int Target { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		writer.WriteInt(Source);
		writer.WriteInt(Target);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		Source = reader.ReadInt();
		Target = reader.ReadInt();
	}
}
