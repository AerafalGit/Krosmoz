// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Types.Game.Context.Roleplay.Job;

public sealed class JobCrafterDirectoryEntryPlayerInfo : DofusType
{
	public new const ushort StaticProtocolId = 194;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static JobCrafterDirectoryEntryPlayerInfo Empty =>
		new() { PlayerId = 0, PlayerName = string.Empty, AlignmentSide = 0, Breed = 0, Sex = false, IsInWorkshop = false, WorldX = 0, WorldY = 0, MapId = 0, SubAreaId = 0 };

	public required int PlayerId { get; set; }

	public required string PlayerName { get; set; }

	public required sbyte AlignmentSide { get; set; }

	public required sbyte Breed { get; set; }

	public required bool Sex { get; set; }

	public required bool IsInWorkshop { get; set; }

	public required short WorldX { get; set; }

	public required short WorldY { get; set; }

	public required int MapId { get; set; }

	public required short SubAreaId { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteInt(PlayerId);
		writer.WriteUtfLengthPrefixed16(PlayerName);
		writer.WriteSByte(AlignmentSide);
		writer.WriteSByte(Breed);
		writer.WriteBoolean(Sex);
		writer.WriteBoolean(IsInWorkshop);
		writer.WriteShort(WorldX);
		writer.WriteShort(WorldY);
		writer.WriteInt(MapId);
		writer.WriteShort(SubAreaId);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		PlayerId = reader.ReadInt();
		PlayerName = reader.ReadUtfLengthPrefixed16();
		AlignmentSide = reader.ReadSByte();
		Breed = reader.ReadSByte();
		Sex = reader.ReadBoolean();
		IsInWorkshop = reader.ReadBoolean();
		WorldX = reader.ReadShort();
		WorldY = reader.ReadShort();
		MapId = reader.ReadInt();
		SubAreaId = reader.ReadShort();
	}
}
