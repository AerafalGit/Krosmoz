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

	public required short SubAreaId { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteInt(MemberId);
		writer.WriteShort(WorldX);
		writer.WriteShort(WorldY);
		writer.WriteInt(MapId);
		writer.WriteShort(SubAreaId);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		MemberId = reader.ReadInt();
		WorldX = reader.ReadShort();
		WorldY = reader.ReadShort();
		MapId = reader.ReadInt();
		SubAreaId = reader.ReadShort();
	}
}
