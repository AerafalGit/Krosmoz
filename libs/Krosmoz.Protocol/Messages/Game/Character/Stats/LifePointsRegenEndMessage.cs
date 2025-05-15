// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Character.Stats;

public sealed class LifePointsRegenEndMessage : UpdateLifePointsMessage
{
	public new const uint StaticProtocolId = 5686;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static new LifePointsRegenEndMessage Empty =>
		new() { MaxLifePoints = 0, LifePoints = 0, LifePointsGained = 0 };

	public required int LifePointsGained { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		writer.WriteInt32(LifePointsGained);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		LifePointsGained = reader.ReadInt32();
	}
}
