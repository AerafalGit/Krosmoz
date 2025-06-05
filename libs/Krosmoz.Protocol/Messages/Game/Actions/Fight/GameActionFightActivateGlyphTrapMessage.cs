// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Actions.Fight;

public sealed class GameActionFightActivateGlyphTrapMessage : AbstractGameActionMessage
{
	public new const uint StaticProtocolId = 6545;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static new GameActionFightActivateGlyphTrapMessage Empty =>
		new() { SourceId = 0, ActionId = 0, MarkId = 0, Active = false };

	public required short MarkId { get; set; }

	public required bool Active { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		writer.WriteInt16(MarkId);
		writer.WriteBoolean(Active);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		MarkId = reader.ReadInt16();
		Active = reader.ReadBoolean();
	}
}
