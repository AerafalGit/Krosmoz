// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Actions.Fight;

public sealed class GameActionFightTriggerGlyphTrapMessage : AbstractGameActionMessage
{
	public new const uint StaticProtocolId = 5741;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static new GameActionFightTriggerGlyphTrapMessage Empty =>
		new() { SourceId = 0, ActionId = 0, MarkId = 0, TriggeringCharacterId = 0, TriggeredSpellId = 0 };

	public required short MarkId { get; set; }

	public required int TriggeringCharacterId { get; set; }

	public required ushort TriggeredSpellId { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		writer.WriteInt16(MarkId);
		writer.WriteInt32(TriggeringCharacterId);
		writer.WriteVarUInt16(TriggeredSpellId);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		MarkId = reader.ReadInt16();
		TriggeringCharacterId = reader.ReadInt32();
		TriggeredSpellId = reader.ReadVarUInt16();
	}
}
