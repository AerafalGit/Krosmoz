// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Actions.Fight;

public sealed class GameActionFightStateChangeMessage : AbstractGameActionMessage
{
	public new const uint StaticProtocolId = 5569;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static new GameActionFightStateChangeMessage Empty =>
		new() { SourceId = 0, ActionId = 0, TargetId = 0, StateId = 0, Active = false };

	public required int TargetId { get; set; }

	public required short StateId { get; set; }

	public required bool Active { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		writer.WriteInt(TargetId);
		writer.WriteShort(StateId);
		writer.WriteBoolean(Active);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		TargetId = reader.ReadInt();
		StateId = reader.ReadShort();
		Active = reader.ReadBoolean();
	}
}
