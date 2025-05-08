// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Character.Stats;

public class UpdateLifePointsMessage : DofusMessage
{
	public new const uint StaticProtocolId = 5658;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static UpdateLifePointsMessage Empty =>
		new() { LifePoints = 0, MaxLifePoints = 0 };

	public required int LifePoints { get; set; }

	public required int MaxLifePoints { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteInt(LifePoints);
		writer.WriteInt(MaxLifePoints);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		LifePoints = reader.ReadInt();
		MaxLifePoints = reader.ReadInt();
	}
}
