// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Game.Actions.Fight;

namespace Krosmoz.Protocol.Messages.Game.Actions.Fight;

public sealed class GameActionFightMarkCellsMessage : AbstractGameActionMessage
{
	public new const uint StaticProtocolId = 5540;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static new GameActionFightMarkCellsMessage Empty =>
		new() { SourceId = 0, ActionId = 0, Mark = GameActionMark.Empty };

	public required GameActionMark Mark { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		Mark.Serialize(writer);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		Mark = GameActionMark.Empty;
		Mark.Deserialize(reader);
	}
}
