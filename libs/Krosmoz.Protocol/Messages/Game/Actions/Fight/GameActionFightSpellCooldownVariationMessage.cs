// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Actions.Fight;

public sealed class GameActionFightSpellCooldownVariationMessage : AbstractGameActionMessage
{
	public new const uint StaticProtocolId = 6219;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static new GameActionFightSpellCooldownVariationMessage Empty =>
		new() { SourceId = 0, ActionId = 0, TargetId = 0, SpellId = 0, Value = 0 };

	public required int TargetId { get; set; }

	public required int SpellId { get; set; }

	public required short Value { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		writer.WriteInt(TargetId);
		writer.WriteInt(SpellId);
		writer.WriteShort(Value);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		TargetId = reader.ReadInt();
		SpellId = reader.ReadInt();
		Value = reader.ReadShort();
	}
}
