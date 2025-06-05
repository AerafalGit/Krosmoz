// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Actions.Fight;

public sealed class GameActionFightLifeAndShieldPointsLostMessage : GameActionFightLifePointsLostMessage
{
	public new const uint StaticProtocolId = 6310;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static new GameActionFightLifeAndShieldPointsLostMessage Empty =>
		new() { SourceId = 0, ActionId = 0, PermanentDamages = 0, Loss = 0, TargetId = 0, ShieldLoss = 0 };

	public required ushort ShieldLoss { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		writer.WriteVarUInt16(ShieldLoss);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		ShieldLoss = reader.ReadVarUInt16();
	}
}
