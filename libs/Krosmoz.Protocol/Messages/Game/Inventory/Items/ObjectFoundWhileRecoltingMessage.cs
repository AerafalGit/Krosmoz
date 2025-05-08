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
		new() { GenericId = 0, Quantity = 0, RessourceGenericId = 0 };

	public required int GenericId { get; set; }

	public required int Quantity { get; set; }

	public required int RessourceGenericId { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteInt(GenericId);
		writer.WriteInt(Quantity);
		writer.WriteInt(RessourceGenericId);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		GenericId = reader.ReadInt();
		Quantity = reader.ReadInt();
		RessourceGenericId = reader.ReadInt();
	}
}
