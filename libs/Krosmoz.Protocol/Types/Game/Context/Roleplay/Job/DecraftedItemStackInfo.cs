// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Types.Game.Context.Roleplay.Job;

public sealed class DecraftedItemStackInfo : DofusType
{
	public new const ushort StaticProtocolId = 481;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static DecraftedItemStackInfo Empty =>
		new() { ObjectUID = 0, BonusMin = 0, BonusMax = 0, RunesId = [], RunesQty = [] };

	public required uint ObjectUID { get; set; }

	public required float BonusMin { get; set; }

	public required float BonusMax { get; set; }

	public required IEnumerable<ushort> RunesId { get; set; }

	public required IEnumerable<uint> RunesQty { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteVarUInt32(ObjectUID);
		writer.WriteSingle(BonusMin);
		writer.WriteSingle(BonusMax);
		var runesIdBefore = writer.Position;
		var runesIdCount = 0;
		writer.WriteInt16(0);
		foreach (var item in RunesId)
		{
			writer.WriteVarUInt16(item);
			runesIdCount++;
		}
		var runesIdAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, runesIdBefore);
		writer.WriteInt16((short)runesIdCount);
		writer.Seek(SeekOrigin.Begin, runesIdAfter);
		var runesQtyBefore = writer.Position;
		var runesQtyCount = 0;
		writer.WriteInt16(0);
		foreach (var item in RunesQty)
		{
			writer.WriteVarUInt32(item);
			runesQtyCount++;
		}
		var runesQtyAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, runesQtyBefore);
		writer.WriteInt16((short)runesQtyCount);
		writer.Seek(SeekOrigin.Begin, runesQtyAfter);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		ObjectUID = reader.ReadVarUInt32();
		BonusMin = reader.ReadSingle();
		BonusMax = reader.ReadSingle();
		var runesIdCount = reader.ReadInt16();
		var runesId = new ushort[runesIdCount];
		for (var i = 0; i < runesIdCount; i++)
		{
			runesId[i] = reader.ReadVarUInt16();
		}
		RunesId = runesId;
		var runesQtyCount = reader.ReadInt16();
		var runesQty = new uint[runesQtyCount];
		for (var i = 0; i < runesQtyCount; i++)
		{
			runesQty[i] = reader.ReadVarUInt32();
		}
		RunesQty = runesQty;
	}
}
