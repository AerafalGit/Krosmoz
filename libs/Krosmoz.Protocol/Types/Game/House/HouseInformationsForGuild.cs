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

	public required int HouseId { get; set; }

	public required int ModelId { get; set; }

	public required string OwnerName { get; set; }

	public required short WorldX { get; set; }

	public required short WorldY { get; set; }

	public required int MapId { get; set; }

	public required short SubAreaId { get; set; }

	public required IEnumerable<int> SkillListIds { get; set; }

	public required uint GuildshareParams { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteInt(HouseId);
		writer.WriteInt(ModelId);
		writer.WriteUtfLengthPrefixed16(OwnerName);
		writer.WriteShort(WorldX);
		writer.WriteShort(WorldY);
		writer.WriteInt(MapId);
		writer.WriteShort(SubAreaId);
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
		writer.WriteUInt(GuildshareParams);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		HouseId = reader.ReadInt();
		ModelId = reader.ReadInt();
		OwnerName = reader.ReadUtfLengthPrefixed16();
		WorldX = reader.ReadShort();
		WorldY = reader.ReadShort();
		MapId = reader.ReadInt();
		SubAreaId = reader.ReadShort();
		var skillListIdsCount = reader.ReadShort();
		var skillListIds = new int[skillListIdsCount];
		for (var i = 0; i < skillListIdsCount; i++)
		{
			skillListIds[i] = reader.ReadInt();
		}
		SkillListIds = skillListIds;
		GuildshareParams = reader.ReadUInt();
	}
}
