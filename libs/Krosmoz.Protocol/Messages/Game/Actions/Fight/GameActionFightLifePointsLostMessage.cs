// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Actions.Fight;

public class GameActionFightLifePointsLostMessage : AbstractGameActionMessage
{
	public new const uint StaticProtocolId = 6312;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static new GameActionFightLifePointsLostMessage Empty =>
		new() { SourceId = 0, ActionId = 0, TargetId = 0, Loss = 0, PermanentDamages = 0 };

	public required int TargetId { get; set; }

	public required short Loss { get; set; }

	public required short PermanentDamages { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		writer.WriteInt(TargetId);
		writer.WriteShort(Loss);
		writer.WriteShort(PermanentDamages);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		TargetId = reader.ReadInt();
		Loss = reader.ReadShort();
		PermanentDamages = reader.ReadShort();
	}
}
