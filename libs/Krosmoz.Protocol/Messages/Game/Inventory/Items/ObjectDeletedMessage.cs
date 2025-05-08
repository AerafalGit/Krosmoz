// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Inventory.Items;

public sealed class ObjectDeletedMessage : DofusMessage
{
	public new const uint StaticProtocolId = 3024;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static ObjectDeletedMessage Empty =>
		new() { ObjectUID = 0 };

	public required int ObjectUID { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteInt(ObjectUID);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		ObjectUID = reader.ReadInt();
	}
}
