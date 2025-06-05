// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Inventory.Exchanges;

public sealed class ExchangeReadyMessage : DofusMessage
{
	public new const uint StaticProtocolId = 5511;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static ExchangeReadyMessage Empty =>
		new() { Ready = false, Step = 0 };

	public required bool Ready { get; set; }

	public required ushort Step { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteBoolean(Ready);
		writer.WriteVarUInt16(Step);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		Ready = reader.ReadBoolean();
		Step = reader.ReadVarUInt16();
	}
}
