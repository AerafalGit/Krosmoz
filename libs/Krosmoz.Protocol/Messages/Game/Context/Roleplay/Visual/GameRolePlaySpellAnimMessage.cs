// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Context.Roleplay.Visual;

public sealed class GameRolePlaySpellAnimMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6114;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static GameRolePlaySpellAnimMessage Empty =>
		new() { CasterId = 0, TargetCellId = 0, SpellId = 0, SpellLevel = 0 };

	public required int CasterId { get; set; }

	public required short TargetCellId { get; set; }

	public required short SpellId { get; set; }

	public required sbyte SpellLevel { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteInt(CasterId);
		writer.WriteShort(TargetCellId);
		writer.WriteShort(SpellId);
		writer.WriteSByte(SpellLevel);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		CasterId = reader.ReadInt();
		TargetCellId = reader.ReadShort();
		SpellId = reader.ReadShort();
		SpellLevel = reader.ReadSByte();
	}
}
