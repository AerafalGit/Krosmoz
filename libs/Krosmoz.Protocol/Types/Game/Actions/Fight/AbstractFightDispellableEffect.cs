// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Types.Game.Actions.Fight;

public class AbstractFightDispellableEffect : DofusType
{
	public new const ushort StaticProtocolId = 206;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static AbstractFightDispellableEffect Empty =>
		new() { Uid = 0, TargetId = 0, TurnDuration = 0, Dispelable = 0, SpellId = 0, ParentBoostUid = 0 };

	public required int Uid { get; set; }

	public required int TargetId { get; set; }

	public required short TurnDuration { get; set; }

	public required sbyte Dispelable { get; set; }

	public required short SpellId { get; set; }

	public required int ParentBoostUid { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteInt(Uid);
		writer.WriteInt(TargetId);
		writer.WriteShort(TurnDuration);
		writer.WriteSByte(Dispelable);
		writer.WriteShort(SpellId);
		writer.WriteInt(ParentBoostUid);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		Uid = reader.ReadInt();
		TargetId = reader.ReadInt();
		TurnDuration = reader.ReadShort();
		Dispelable = reader.ReadSByte();
		SpellId = reader.ReadShort();
		ParentBoostUid = reader.ReadInt();
	}
}
