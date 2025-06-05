// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Inventory.Exchanges;

public class ExchangeCraftResultMessage : DofusMessage
{
	public new const uint StaticProtocolId = 5790;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static ExchangeCraftResultMessage Empty =>
		new() { CraftResult = 0 };

	public required sbyte CraftResult { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteInt8(CraftResult);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		CraftResult = reader.ReadInt8();
	}
}
