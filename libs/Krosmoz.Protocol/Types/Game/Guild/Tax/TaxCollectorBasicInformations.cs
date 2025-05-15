// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Types.Game.Guild.Tax;

public sealed class TaxCollectorBasicInformations : DofusType
{
	public new const ushort StaticProtocolId = 96;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static TaxCollectorBasicInformations Empty =>
		new() { FirstNameId = 0, LastNameId = 0, WorldX = 0, WorldY = 0, MapId = 0, SubAreaId = 0 };

	public required short FirstNameId { get; set; }

	public required short LastNameId { get; set; }

	public required short WorldX { get; set; }

	public required short WorldY { get; set; }

	public required int MapId { get; set; }

	public required short SubAreaId { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteInt16(FirstNameId);
		writer.WriteInt16(LastNameId);
		writer.WriteInt16(WorldX);
		writer.WriteInt16(WorldY);
		writer.WriteInt32(MapId);
		writer.WriteInt16(SubAreaId);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		FirstNameId = reader.ReadInt16();
		LastNameId = reader.ReadInt16();
		WorldX = reader.ReadInt16();
		WorldY = reader.ReadInt16();
		MapId = reader.ReadInt32();
		SubAreaId = reader.ReadInt16();
	}
}
