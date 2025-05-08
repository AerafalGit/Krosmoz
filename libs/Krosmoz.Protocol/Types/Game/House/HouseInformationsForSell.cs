// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Types.Game.House;

public sealed class HouseInformationsForSell : DofusType
{
	public new const ushort StaticProtocolId = 221;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static HouseInformationsForSell Empty =>
		new() { ModelId = 0, OwnerName = string.Empty, OwnerConnected = false, WorldX = 0, WorldY = 0, SubAreaId = 0, NbRoom = 0, NbChest = 0, SkillListIds = [], IsLocked = false, Price = 0 };

	public required int ModelId { get; set; }

	public required string OwnerName { get; set; }

	public required bool OwnerConnected { get; set; }

	public required short WorldX { get; set; }

	public required short WorldY { get; set; }

	public required short SubAreaId { get; set; }

	public required sbyte NbRoom { get; set; }

	public required sbyte NbChest { get; set; }

	public required IEnumerable<int> SkillListIds { get; set; }

	public required bool IsLocked { get; set; }

	public required int Price { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteInt(ModelId);
		writer.WriteUtfLengthPrefixed16(OwnerName);
		writer.WriteBoolean(OwnerConnected);
		writer.WriteShort(WorldX);
		writer.WriteShort(WorldY);
		writer.WriteShort(SubAreaId);
		writer.WriteSByte(NbRoom);
		writer.WriteSByte(NbChest);
		var skillListIdsBefore = writer.Position;
		var skillListIdsCount = 0;
		writer.WriteShort(0);
		foreach (var item in SkillListIds)
		{
			writer.WriteInt(item);
			skillListIdsCount++;
		}
		var skillListIdsAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, skillListIdsBefore);
		writer.WriteShort((short)skillListIdsCount);
		writer.Seek(SeekOrigin.Begin, skillListIdsAfter);
		writer.WriteBoolean(IsLocked);
		writer.WriteInt(Price);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		ModelId = reader.ReadInt();
		OwnerName = reader.ReadUtfLengthPrefixed16();
		OwnerConnected = reader.ReadBoolean();
		WorldX = reader.ReadShort();
		WorldY = reader.ReadShort();
		SubAreaId = reader.ReadShort();
		NbRoom = reader.ReadSByte();
		NbChest = reader.ReadSByte();
		var skillListIdsCount = reader.ReadShort();
		var skillListIds = new int[skillListIdsCount];
		for (var i = 0; i < skillListIdsCount; i++)
		{
			skillListIds[i] = reader.ReadInt();
		}
		SkillListIds = skillListIds;
		IsLocked = reader.ReadBoolean();
		Price = reader.ReadInt();
	}
}
