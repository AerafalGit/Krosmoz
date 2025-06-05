// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Inventory.Exchanges;

public class ExchangeObjectMessage : DofusMessage
{
	public new const uint StaticProtocolId = 5515;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static ExchangeObjectMessage Empty =>
		new() { Remote = false };

	public required bool Remote { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteBoolean(Remote);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		Remote = reader.ReadBoolean();
	}
}
