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

	public required IEnumerable<ushort> PositionsForChallengers { get; set; }

	public required IEnumerable<ushort> PositionsForDefenders { get; set; }

	public required sbyte TeamNumber { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		var positionsForChallengersBefore = writer.Position;
		var positionsForChallengersCount = 0;
		writer.WriteInt16(0);
		foreach (var item in PositionsForChallengers)
		{
			writer.WriteVarUInt16(item);
			positionsForChallengersCount++;
		}
		var positionsForChallengersAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, positionsForChallengersBefore);
		writer.WriteInt16((short)positionsForChallengersCount);
		writer.Seek(SeekOrigin.Begin, positionsForChallengersAfter);
		var positionsForDefendersBefore = writer.Position;
		var positionsForDefendersCount = 0;
		writer.WriteInt16(0);
		foreach (var item in PositionsForDefenders)
		{
			writer.WriteVarUInt16(item);
			positionsForDefendersCount++;
		}
		var positionsForDefendersAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, positionsForDefendersBefore);
		writer.WriteInt16((short)positionsForDefendersCount);
		writer.Seek(SeekOrigin.Begin, positionsForDefendersAfter);
		writer.WriteInt8(TeamNumber);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		var positionsForChallengersCount = reader.ReadInt16();
		var positionsForChallengers = new ushort[positionsForChallengersCount];
		for (var i = 0; i < positionsForChallengersCount; i++)
		{
			positionsForChallengers[i] = reader.ReadVarUInt16();
		}
		PositionsForChallengers = positionsForChallengers;
		var positionsForDefendersCount = reader.ReadInt16();
		var positionsForDefenders = new ushort[positionsForDefendersCount];
		for (var i = 0; i < positionsForDefendersCount; i++)
		{
			positionsForDefenders[i] = reader.ReadVarUInt16();
		}
		PositionsForDefenders = positionsForDefenders;
		TeamNumber = reader.ReadInt8();
	}
}
