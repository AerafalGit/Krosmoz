// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Inventory.Exchanges;

public sealed class ExchangeStartedWithStorageMessage : ExchangeStartedMessage
{
	public new const uint StaticProtocolId = 6236;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static new ExchangeStartedWithStorageMessage Empty =>
		new() { ExchangeType = 0, StorageMaxSlot = 0 };

	public required uint StorageMaxSlot { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		writer.WriteVarUInt32(StorageMaxSlot);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		StorageMaxSlot = reader.ReadVarUInt32();
	}
}
