// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Types.Game.Context.Roleplay.TreasureHunt;

public sealed class TreasureHuntStepFollowDirectionToPOI : TreasureHuntStep
{
	public new const ushort StaticProtocolId = 461;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static new TreasureHuntStepFollowDirectionToPOI Empty =>
		new() { Direction = 0, PoiLabelId = 0 };

	public required sbyte Direction { get; set; }

	public required ushort PoiLabelId { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		writer.WriteInt8(Direction);
		writer.WriteVarUInt16(PoiLabelId);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		Direction = reader.ReadInt8();
		PoiLabelId = reader.ReadVarUInt16();
	}
}
