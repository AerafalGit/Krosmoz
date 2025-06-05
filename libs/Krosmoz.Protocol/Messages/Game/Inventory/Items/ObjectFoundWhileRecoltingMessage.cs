// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Inventory.Items;

public sealed class ObjectFoundWhileRecoltingMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6017;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static ObjectFoundWhileRecoltingMessage Empty =>
		new() { GenericId = 0, Quantity = 0, ResourceGenericId = 0 };

	public required ushort GenericId { get; set; }

	public required uint Quantity { get; set; }

	public required uint ResourceGenericId { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteVarUInt16(GenericId);
		writer.WriteVarUInt32(Quantity);
		writer.WriteVarUInt32(ResourceGenericId);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		GenericId = reader.ReadVarUInt16();
		Quantity = reader.ReadVarUInt32();
		ResourceGenericId = reader.ReadVarUInt32();
	}
}
