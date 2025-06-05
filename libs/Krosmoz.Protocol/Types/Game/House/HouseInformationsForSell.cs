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

	public required uint ModelId { get; set; }

	public required string OwnerName { get; set; }

	public required bool OwnerConnected { get; set; }

	public required short WorldX { get; set; }

	public required short WorldY { get; set; }

	public required ushort SubAreaId { get; set; }

	public required sbyte NbRoom { get; set; }

	public required sbyte NbChest { get; set; }

	public required IEnumerable<int> SkillListIds { get; set; }

	public required bool IsLocked { get; set; }

	public required uint Price { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteVarUInt32(ModelId);
		writer.WriteUtfPrefixedLength16(OwnerName);
		writer.WriteBoolean(OwnerConnected);
		writer.WriteInt16(WorldX);
		writer.WriteInt16(WorldY);
		writer.WriteVarUInt16(SubAreaId);
		writer.WriteInt8(NbRoom);
		writer.WriteInt8(NbChest);
		var skillListIdsBefore = writer.Position;
		var skillListIdsCount = 0;
		writer.WriteInt16(0);
		foreach (var item in SkillListIds)
		{
			writer.WriteInt32(item);
			skillListIdsCount++;
		}
		var skillListIdsAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, skillListIdsBefore);
		writer.WriteInt16((short)skillListIdsCount);
		writer.Seek(SeekOrigin.Begin, skillListIdsAfter);
		writer.WriteBoolean(IsLocked);
		writer.WriteVarUInt32(Price);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		ModelId = reader.ReadVarUInt32();
		OwnerName = reader.ReadUtfPrefixedLength16();
		OwnerConnected = reader.ReadBoolean();
		WorldX = reader.ReadInt16();
		WorldY = reader.ReadInt16();
		SubAreaId = reader.ReadVarUInt16();
		NbRoom = reader.ReadInt8();
		NbChest = reader.ReadInt8();
		var skillListIdsCount = reader.ReadInt16();
		var skillListIds = new int[skillListIdsCount];
		for (var i = 0; i < skillListIdsCount; i++)
		{
			skillListIds[i] = reader.ReadInt32();
		}
		SkillListIds = skillListIds;
		IsLocked = reader.ReadBoolean();
		Price = reader.ReadVarUInt32();
	}
}
