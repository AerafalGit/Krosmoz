// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Inventory.Exchanges;

public sealed class ExchangeWaitingResultMessage : DofusMessage
{
	public new const uint StaticProtocolId = 5786;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static ExchangeWaitingResultMessage Empty =>
		new() { Bwait = false };

	public required bool Bwait { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteBoolean(Bwait);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		Bwait = reader.ReadBoolean();
	}
}
