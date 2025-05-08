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

	public required short MsgId { get; set; }

	public required uint TimeStamp { get; set; }

	public required string Owner { get; set; }

	public required uint ObjectGenericId { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteShort(MsgId);
		writer.WriteUInt(TimeStamp);
		writer.WriteUtfLengthPrefixed16(Owner);
		writer.WriteUInt(ObjectGenericId);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		MsgId = reader.ReadShort();
		TimeStamp = reader.ReadUInt();
		Owner = reader.ReadUtfLengthPrefixed16();
		ObjectGenericId = reader.ReadUInt();
	}
}
