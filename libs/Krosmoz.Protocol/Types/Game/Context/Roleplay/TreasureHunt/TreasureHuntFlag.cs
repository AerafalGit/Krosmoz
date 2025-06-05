// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Types.Game.Context.Roleplay.TreasureHunt;

public sealed class TreasureHuntFlag : DofusType
{
	public new const ushort StaticProtocolId = 473;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static TreasureHuntFlag Empty =>
		new() { MapId = 0, State = 0 };

	public required int MapId { get; set; }

	public required sbyte State { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteInt32(MapId);
		writer.WriteInt8(State);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		MapId = reader.ReadInt32();
		State = reader.ReadInt8();
	}
}
