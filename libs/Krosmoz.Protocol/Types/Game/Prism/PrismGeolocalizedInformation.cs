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
		writer.WriteShort(WorldX);
		writer.WriteShort(WorldY);
		writer.WriteInt(MapId);
		writer.WriteUShort(Prism.ProtocolId);
		Prism.Serialize(writer);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		WorldX = reader.ReadShort();
		WorldY = reader.ReadShort();
		MapId = reader.ReadInt();
		Prism = Types.TypeFactory.CreateType<PrismInformation>(reader.ReadUShort());
		Prism.Deserialize(reader);
	}
}
