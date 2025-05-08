// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Inventory.Items;

public sealed class ObjectUseOnCharacterMessage : ObjectUseMessage
{
	public new const uint StaticProtocolId = 3003;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static new ObjectUseOnCharacterMessage Empty =>
		new() { ObjectUID = 0, CharacterId = 0 };

	public required int CharacterId { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		writer.WriteInt(CharacterId);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		CharacterId = reader.ReadInt();
	}
}
