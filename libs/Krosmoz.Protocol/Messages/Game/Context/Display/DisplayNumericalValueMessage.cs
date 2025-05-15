// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Context.Display;

public class DisplayNumericalValueMessage : DofusMessage
{
	public new const uint StaticProtocolId = 5808;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static DisplayNumericalValueMessage Empty =>
		new() { EntityId = 0, Value = 0, Type = 0 };

	public required int EntityId { get; set; }

	public required int Value { get; set; }

	public required sbyte Type { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteInt32(EntityId);
		writer.WriteInt32(Value);
		writer.WriteInt8(Type);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		EntityId = reader.ReadInt32();
		Value = reader.ReadInt32();
		Type = reader.ReadInt8();
	}
}
