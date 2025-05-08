// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Inventory.Exchanges;

public sealed class ExchangeBidHouseGenericItemRemovedMessage : DofusMessage
{
	public new const uint StaticProtocolId = 5948;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static ExchangeBidHouseGenericItemRemovedMessage Empty =>
		new() { ObjGenericId = 0 };

	public required int ObjGenericId { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteInt(ObjGenericId);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		ObjGenericId = reader.ReadInt();
	}
}
