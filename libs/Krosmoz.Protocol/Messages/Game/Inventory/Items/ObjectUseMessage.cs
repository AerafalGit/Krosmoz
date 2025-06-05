// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Inventory.Items;

public class ObjectUseMessage : DofusMessage
{
	public new const uint StaticProtocolId = 3019;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static ObjectUseMessage Empty =>
		new() { ObjectUID = 0 };

	public required uint ObjectUID { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteVarUInt32(ObjectUID);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		ObjectUID = reader.ReadVarUInt32();
	}
}
