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

	public required ushort SubAreaId { get; set; }

	public required sbyte NbMount { get; set; }

	public required sbyte NbObject { get; set; }

	public required uint Price { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteUtfPrefixedLength16(GuildOwner);
		writer.WriteInt16(WorldX);
		writer.WriteInt16(WorldY);
		writer.WriteVarUInt16(SubAreaId);
		writer.WriteInt8(NbMount);
		writer.WriteInt8(NbObject);
		writer.WriteVarUInt32(Price);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		GuildOwner = reader.ReadUtfPrefixedLength16();
		WorldX = reader.ReadInt16();
		WorldY = reader.ReadInt16();
		SubAreaId = reader.ReadVarUInt16();
		NbMount = reader.ReadInt8();
		NbObject = reader.ReadInt8();
		Price = reader.ReadVarUInt32();
	}
}
