// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Actions.Fight;

public sealed class GameActionFightExchangePositionsMessage : AbstractGameActionMessage
{
	public new const uint StaticProtocolId = 5527;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static new GameActionFightExchangePositionsMessage Empty =>
		new() { SourceId = 0, ActionId = 0, TargetId = 0, CasterCellId = 0, TargetCellId = 0 };

	public required int TargetId { get; set; }

	public required short CasterCellId { get; set; }

	public required short TargetCellId { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		writer.WriteInt32(TargetId);
		writer.WriteInt16(CasterCellId);
		writer.WriteInt16(TargetCellId);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		TargetId = reader.ReadInt32();
		CasterCellId = reader.ReadInt16();
		TargetCellId = reader.ReadInt16();
	}
}
