// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Context.Fight.Challenge;

public sealed class ChallengeTargetsListMessage : DofusMessage
{
	public new const uint StaticProtocolId = 5613;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static ChallengeTargetsListMessage Empty =>
		new() { TargetIds = [], TargetCells = [] };

	public required IEnumerable<int> TargetIds { get; set; }

	public required IEnumerable<short> TargetCells { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		var targetIdsBefore = writer.Position;
		var targetIdsCount = 0;
		writer.WriteInt16(0);
		foreach (var item in TargetIds)
		{
			writer.WriteInt32(item);
			targetIdsCount++;
		}
		var targetIdsAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, targetIdsBefore);
		writer.WriteInt16((short)targetIdsCount);
		writer.Seek(SeekOrigin.Begin, targetIdsAfter);
		var targetCellsBefore = writer.Position;
		var targetCellsCount = 0;
		writer.WriteInt16(0);
		foreach (var item in TargetCells)
		{
			writer.WriteInt16(item);
			targetCellsCount++;
		}
		var targetCellsAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, targetCellsBefore);
		writer.WriteInt16((short)targetCellsCount);
		writer.Seek(SeekOrigin.Begin, targetCellsAfter);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		var targetIdsCount = reader.ReadInt16();
		var targetIds = new int[targetIdsCount];
		for (var i = 0; i < targetIdsCount; i++)
		{
			targetIds[i] = reader.ReadInt32();
		}
		TargetIds = targetIds;
		var targetCellsCount = reader.ReadInt16();
		var targetCells = new short[targetCellsCount];
		for (var i = 0; i < targetCellsCount; i++)
		{
			targetCells[i] = reader.ReadInt16();
		}
		TargetCells = targetCells;
	}
}
