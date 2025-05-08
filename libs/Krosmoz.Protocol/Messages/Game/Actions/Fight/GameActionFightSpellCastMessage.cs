// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Actions.Fight;

public sealed class GameActionFightSpellCastMessage : AbstractGameActionFightTargetedAbilityMessage
{
	public new const uint StaticProtocolId = 1010;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static new GameActionFightSpellCastMessage Empty =>
		new() { SourceId = 0, ActionId = 0, SilentCast = false, Critical = 0, DestinationCellId = 0, TargetId = 0, SpellId = 0, SpellLevel = 0 };

	public required short SpellId { get; set; }

	public required sbyte SpellLevel { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		writer.WriteShort(SpellId);
		writer.WriteSByte(SpellLevel);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		SpellId = reader.ReadShort();
		SpellLevel = reader.ReadSByte();
	}
}
