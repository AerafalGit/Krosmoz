// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Types.Game.Mount;

public sealed class ItemDurability : DofusType
{
	public new const ushort StaticProtocolId = 168;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static ItemDurability Empty =>
		new() { Durability = 0, DurabilityMax = 0 };

	public required short Durability { get; set; }

	public required short DurabilityMax { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteInt16(Durability);
		writer.WriteInt16(DurabilityMax);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		Durability = reader.ReadInt16();
		DurabilityMax = reader.ReadInt16();
	}
}
