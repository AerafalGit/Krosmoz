// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Inventory.Exchanges;

public sealed class ExchangeIsReadyMessage : DofusMessage
{
	public new const uint StaticProtocolId = 5509;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static ExchangeIsReadyMessage Empty =>
		new() { Id = 0, Ready = false };

	public required uint Id { get; set; }

	public required bool Ready { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteVarUInt32(Id);
		writer.WriteBoolean(Ready);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		Id = reader.ReadVarUInt32();
		Ready = reader.ReadBoolean();
	}
}
