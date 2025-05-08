// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Types.Game.Data.Items;

public sealed class ObjectItemQuantity : Item
{
	public new const ushort StaticProtocolId = 119;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static new ObjectItemQuantity Empty =>
		new() { ObjectUID = 0, Quantity = 0 };

	public required int ObjectUID { get; set; }

	public required int Quantity { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		writer.WriteInt(ObjectUID);
		writer.WriteInt(Quantity);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		ObjectUID = reader.ReadInt();
		Quantity = reader.ReadInt();
	}
}
