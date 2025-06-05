// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Actions.Fight;

public sealed class GameActionFightSlideMessage : AbstractGameActionMessage
{
	public new const uint StaticProtocolId = 5525;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static new GameActionFightSlideMessage Empty =>
		new() { SourceId = 0, ActionId = 0, TargetId = 0, StartCellId = 0, EndCellId = 0 };

	public required int TargetId { get; set; }

	public required short StartCellId { get; set; }

	public required short EndCellId { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		writer.WriteInt32(TargetId);
		writer.WriteInt16(StartCellId);
		writer.WriteInt16(EndCellId);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		TargetId = reader.ReadInt32();
		StartCellId = reader.ReadInt16();
		EndCellId = reader.ReadInt16();
	}
}
