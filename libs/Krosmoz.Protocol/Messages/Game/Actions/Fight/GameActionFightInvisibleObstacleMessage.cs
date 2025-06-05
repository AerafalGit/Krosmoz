// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Actions.Fight;

public sealed class GameActionFightInvisibleObstacleMessage : AbstractGameActionMessage
{
	public new const uint StaticProtocolId = 5820;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static new GameActionFightInvisibleObstacleMessage Empty =>
		new() { SourceId = 0, ActionId = 0, SourceSpellId = 0 };

	public required uint SourceSpellId { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		writer.WriteVarUInt32(SourceSpellId);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		SourceSpellId = reader.ReadVarUInt32();
	}
}
