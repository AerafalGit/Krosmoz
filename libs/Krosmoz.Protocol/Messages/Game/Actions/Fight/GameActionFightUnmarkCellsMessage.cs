// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Actions.Fight;

public sealed class GameActionFightUnmarkCellsMessage : AbstractGameActionMessage
{
	public new const uint StaticProtocolId = 5570;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static new GameActionFightUnmarkCellsMessage Empty =>
		new() { SourceId = 0, ActionId = 0, MarkId = 0 };

	public required short MarkId { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		writer.WriteInt16(MarkId);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		MarkId = reader.ReadInt16();
	}
}
