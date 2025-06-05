// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Types.Game.Context.Roleplay.TreasureHunt;

public sealed class TreasureHuntStepFollowDirectionToHint : TreasureHuntStep
{
	public new const ushort StaticProtocolId = 472;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static new TreasureHuntStepFollowDirectionToHint Empty =>
		new() { Direction = 0, NpcId = 0 };

	public required sbyte Direction { get; set; }

	public required ushort NpcId { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		writer.WriteInt8(Direction);
		writer.WriteVarUInt16(NpcId);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		Direction = reader.ReadInt8();
		NpcId = reader.ReadVarUInt16();
	}
}
