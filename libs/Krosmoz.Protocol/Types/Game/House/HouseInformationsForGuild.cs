// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Types.Game.House;

public sealed class HouseInformationsForGuild : DofusType
{
	public new const ushort StaticProtocolId = 170;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static HouseInformationsForGuild Empty =>
		new() { HouseId = 0, ModelId = 0, OwnerName = string.Empty, WorldX = 0, WorldY = 0, MapId = 0, SubAreaId = 0, SkillListIds = [], GuildshareParams = 0 };

	public required uint HouseId { get; set; }

	public required uint ModelId { get; set; }

	public required string OwnerName { get; set; }

	public required short WorldX { get; set; }

	public required short WorldY { get; set; }

	public required int MapId { get; set; }

	public required ushort SubAreaId { get; set; }

	public required IEnumerable<int> SkillListIds { get; set; }

	public required uint GuildshareParams { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteVarUInt32(HouseId);
		writer.WriteVarUInt32(ModelId);
		writer.WriteUtfPrefixedLength16(OwnerName);
		writer.WriteInt16(WorldX);
		writer.WriteInt16(WorldY);
		writer.WriteInt32(MapId);
		writer.WriteVarUInt16(SubAreaId);
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
		writer.WriteVarUInt32(GuildshareParams);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		HouseId = reader.ReadVarUInt32();
		ModelId = reader.ReadVarUInt32();
		OwnerName = reader.ReadUtfPrefixedLength16();
		WorldX = reader.ReadInt16();
		WorldY = reader.ReadInt16();
		MapId = reader.ReadInt32();
		SubAreaId = reader.ReadVarUInt16();
		var skillListIdsCount = reader.ReadInt16();
		var skillListIds = new int[skillListIdsCount];
		for (var i = 0; i < skillListIdsCount; i++)
		{
			skillListIds[i] = reader.ReadInt32();
		}
		SkillListIds = skillListIds;
		GuildshareParams = reader.ReadVarUInt32();
	}
}
