// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Inventory.Items;

public sealed class LivingObjectMessageMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6065;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static LivingObjectMessageMessage Empty =>
		new() { MsgId = 0, TimeStamp = 0, Owner = string.Empty, ObjectGenericId = 0 };

	public required ushort MsgId { get; set; }

	public required int TimeStamp { get; set; }

	public required string Owner { get; set; }

	public required ushort ObjectGenericId { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteVarUInt16(MsgId);
		writer.WriteInt32(TimeStamp);
		writer.WriteUtfPrefixedLength16(Owner);
		writer.WriteVarUInt16(ObjectGenericId);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		MsgId = reader.ReadVarUInt16();
		TimeStamp = reader.ReadInt32();
		Owner = reader.ReadUtfPrefixedLength16();
		ObjectGenericId = reader.ReadVarUInt16();
	}
}
