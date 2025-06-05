// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Types.Game.Data.Items;

public sealed class GoldItem : Item
{
	public new const ushort StaticProtocolId = 123;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static new GoldItem Empty =>
		new() { Sum = 0 };

	public required uint Sum { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		writer.WriteVarUInt32(Sum);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		Sum = reader.ReadVarUInt32();
	}
}
