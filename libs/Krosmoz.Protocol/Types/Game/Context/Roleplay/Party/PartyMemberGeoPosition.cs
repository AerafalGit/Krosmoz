// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Types.Game.Context.Roleplay.Party;

public sealed class PartyMemberGeoPosition : DofusType
{
	public new const ushort StaticProtocolId = 378;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static PartyMemberGeoPosition Empty =>
		new() { MemberId = 0, WorldX = 0, WorldY = 0, MapId = 0, SubAreaId = 0 };

	public required int MemberId { get; set; }

	public required short WorldX { get; set; }

	public required short WorldY { get; set; }

	public required int MapId { get; set; }

	public required ushort SubAreaId { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteInt32(MemberId);
		writer.WriteInt16(WorldX);
		writer.WriteInt16(WorldY);
		writer.WriteInt32(MapId);
		writer.WriteVarUInt16(SubAreaId);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		MemberId = reader.ReadInt32();
		WorldX = reader.ReadInt16();
		WorldY = reader.ReadInt16();
		MapId = reader.ReadInt32();
		SubAreaId = reader.ReadVarUInt16();
	}
}
