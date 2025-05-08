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
		writer.WriteShort(FirstNameId);
		writer.WriteShort(LastNameId);
		writer.WriteShort(WorldX);
		writer.WriteShort(WorldY);
		writer.WriteInt(MapId);
		writer.WriteShort(SubAreaId);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		FirstNameId = reader.ReadShort();
		LastNameId = reader.ReadShort();
		WorldX = reader.ReadShort();
		WorldY = reader.ReadShort();
		MapId = reader.ReadInt();
		SubAreaId = reader.ReadShort();
	}
}
