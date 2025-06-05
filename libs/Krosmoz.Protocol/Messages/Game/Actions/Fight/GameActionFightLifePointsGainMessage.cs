// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Actions.Fight;

public sealed class GameActionFightLifePointsGainMessage : AbstractGameActionMessage
{
	public new const uint StaticProtocolId = 6311;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static new GameActionFightLifePointsGainMessage Empty =>
		new() { SourceId = 0, ActionId = 0, TargetId = 0, Delta = 0 };

	public required int TargetId { get; set; }

	public required uint Delta { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		writer.WriteInt32(TargetId);
		writer.WriteVarUInt32(Delta);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		TargetId = reader.ReadInt32();
		Delta = reader.ReadVarUInt32();
	}
}
