// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Types.Game.Prism;

public sealed class PrismGeolocalizedInformation : PrismSubareaEmptyInfo
{
	public new const ushort StaticProtocolId = 434;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static new PrismGeolocalizedInformation Empty =>
		new() { AllianceId = 0, SubAreaId = 0, WorldX = 0, WorldY = 0, MapId = 0, Prism = PrismInformation.Empty };

	public required short WorldX { get; set; }

	public required short WorldY { get; set; }

	public required int MapId { get; set; }

	public required PrismInformation Prism { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		writer.WriteInt16(WorldX);
		writer.WriteInt16(WorldY);
		writer.WriteInt32(MapId);
		writer.WriteUInt16(Prism.ProtocolId);
		Prism.Serialize(writer);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		WorldX = reader.ReadInt16();
		WorldY = reader.ReadInt16();
		MapId = reader.ReadInt32();
		Prism = Types.TypeFactory.CreateType<PrismInformation>(reader.ReadUInt16());
		Prism.Deserialize(reader);
	}
}
