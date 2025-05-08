// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Context.Fight;

public sealed class GameFightPlacementPossiblePositionsMessage : DofusMessage
{
	public new const uint StaticProtocolId = 703;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static GameFightPlacementPossiblePositionsMessage Empty =>
		new() { PositionsForChallengers = [], PositionsForDefenders = [], TeamNumber = 0 };

	public required IEnumerable<short> PositionsForChallengers { get; set; }

	public required IEnumerable<short> PositionsForDefenders { get; set; }

	public required sbyte TeamNumber { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		var positionsForChallengersBefore = writer.Position;
		var positionsForChallengersCount = 0;
		writer.WriteShort(0);
		foreach (var item in PositionsForChallengers)
		{
			writer.WriteShort(item);
			positionsForChallengersCount++;
		}
		var positionsForChallengersAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, positionsForChallengersBefore);
		writer.WriteShort((short)positionsForChallengersCount);
		writer.Seek(SeekOrigin.Begin, positionsForChallengersAfter);
		var positionsForDefendersBefore = writer.Position;
		var positionsForDefendersCount = 0;
		writer.WriteShort(0);
		foreach (var item in PositionsForDefenders)
		{
			writer.WriteShort(item);
			positionsForDefendersCount++;
		}
		var positionsForDefendersAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, positionsForDefendersBefore);
		writer.WriteShort((short)positionsForDefendersCount);
		writer.Seek(SeekOrigin.Begin, positionsForDefendersAfter);
		writer.WriteSByte(TeamNumber);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		var positionsForChallengersCount = reader.ReadShort();
		var positionsForChallengers = new short[positionsForChallengersCount];
		for (var i = 0; i < positionsForChallengersCount; i++)
		{
			positionsForChallengers[i] = reader.ReadShort();
		}
		PositionsForChallengers = positionsForChallengers;
		var positionsForDefendersCount = reader.ReadShort();
		var positionsForDefenders = new short[positionsForDefendersCount];
		for (var i = 0; i < positionsForDefendersCount; i++)
		{
			positionsForDefenders[i] = reader.ReadShort();
		}
		PositionsForDefenders = positionsForDefenders;
		TeamNumber = reader.ReadSByte();
	}
}
