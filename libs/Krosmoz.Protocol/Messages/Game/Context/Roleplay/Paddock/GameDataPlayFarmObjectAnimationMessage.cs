// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Context.Roleplay.Paddock;

public sealed class GameDataPlayFarmObjectAnimationMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6026;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static GameDataPlayFarmObjectAnimationMessage Empty =>
		new() { CellId = [] };

	public required IEnumerable<ushort> CellId { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		var cellIdBefore = writer.Position;
		var cellIdCount = 0;
		writer.WriteInt16(0);
		foreach (var item in CellId)
		{
			writer.WriteVarUInt16(item);
			cellIdCount++;
		}
		var cellIdAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, cellIdBefore);
		writer.WriteInt16((short)cellIdCount);
		writer.Seek(SeekOrigin.Begin, cellIdAfter);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		var cellIdCount = reader.ReadInt16();
		var cellId = new ushort[cellIdCount];
		for (var i = 0; i < cellIdCount; i++)
		{
			cellId[i] = reader.ReadVarUInt16();
		}
		CellId = cellId;
	}
}
