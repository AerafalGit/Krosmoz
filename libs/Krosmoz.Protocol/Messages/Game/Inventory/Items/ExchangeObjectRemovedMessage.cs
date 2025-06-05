// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Messages.Game.Inventory.Exchanges;

namespace Krosmoz.Protocol.Messages.Game.Inventory.Items;

public sealed class ExchangeObjectRemovedMessage : ExchangeObjectMessage
{
	public new const uint StaticProtocolId = 5517;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static new ExchangeObjectRemovedMessage Empty =>
		new() { Remote = false, ObjectUID = 0 };

	public required uint ObjectUID { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		writer.WriteVarUInt32(ObjectUID);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		ObjectUID = reader.ReadVarUInt32();
	}
}
