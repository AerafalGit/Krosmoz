// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Types.Game.Paddock;

public sealed class PaddockInformationsForSell : DofusType
{
	public new const ushort StaticProtocolId = 222;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static PaddockInformationsForSell Empty =>
		new() { GuildOwner = string.Empty, WorldX = 0, WorldY = 0, SubAreaId = 0, NbMount = 0, NbObject = 0, Price = 0 };

	public required string GuildOwner { get; set; }

	public required short WorldX { get; set; }

	public required short WorldY { get; set; }

	public required short SubAreaId { get; set; }

	public required sbyte NbMount { get; set; }

	public required sbyte NbObject { get; set; }

	public required int Price { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteUtfLengthPrefixed16(GuildOwner);
		writer.WriteShort(WorldX);
		writer.WriteShort(WorldY);
		writer.WriteShort(SubAreaId);
		writer.WriteSByte(NbMount);
		writer.WriteSByte(NbObject);
		writer.WriteInt(Price);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		GuildOwner = reader.ReadUtfLengthPrefixed16();
		WorldX = reader.ReadShort();
		WorldY = reader.ReadShort();
		SubAreaId = reader.ReadShort();
		NbMount = reader.ReadSByte();
		NbObject = reader.ReadSByte();
		Price = reader.ReadInt();
	}
}
