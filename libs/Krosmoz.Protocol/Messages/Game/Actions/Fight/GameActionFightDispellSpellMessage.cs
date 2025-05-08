// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Actions.Fight;

public sealed class GameActionFightDispellSpellMessage : GameActionFightDispellMessage
{
	public new const uint StaticProtocolId = 6176;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static new GameActionFightDispellSpellMessage Empty =>
		new() { SourceId = 0, ActionId = 0, TargetId = 0, SpellId = 0 };

	public required int SpellId { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		writer.WriteInt(SpellId);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		SpellId = reader.ReadInt();
	}
}
