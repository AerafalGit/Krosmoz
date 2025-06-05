// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Game.Character.Status;

namespace Krosmoz.Protocol.Types.Game.Context.Roleplay.Job;

public sealed class JobCrafterDirectoryEntryPlayerInfo : DofusType
{
	public new const ushort StaticProtocolId = 194;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static JobCrafterDirectoryEntryPlayerInfo Empty =>
		new() { PlayerId = 0, PlayerName = string.Empty, AlignmentSide = 0, Breed = 0, Sex = false, IsInWorkshop = false, WorldX = 0, WorldY = 0, MapId = 0, SubAreaId = 0, Status = PlayerStatus.Empty };

	public required uint PlayerId { get; set; }

	public required string PlayerName { get; set; }

	public required sbyte AlignmentSide { get; set; }

	public required sbyte Breed { get; set; }

	public required bool Sex { get; set; }

	public required bool IsInWorkshop { get; set; }

	public required short WorldX { get; set; }

	public required short WorldY { get; set; }

	public required int MapId { get; set; }

	public required ushort SubAreaId { get; set; }

	public required PlayerStatus Status { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteVarUInt32(PlayerId);
		writer.WriteUtfPrefixedLength16(PlayerName);
		writer.WriteInt8(AlignmentSide);
		writer.WriteInt8(Breed);
		writer.WriteBoolean(Sex);
		writer.WriteBoolean(IsInWorkshop);
		writer.WriteInt16(WorldX);
		writer.WriteInt16(WorldY);
		writer.WriteInt32(MapId);
		writer.WriteVarUInt16(SubAreaId);
		writer.WriteUInt16(Status.ProtocolId);
		Status.Serialize(writer);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		PlayerId = reader.ReadVarUInt32();
		PlayerName = reader.ReadUtfPrefixedLength16();
		AlignmentSide = reader.ReadInt8();
		Breed = reader.ReadInt8();
		Sex = reader.ReadBoolean();
		IsInWorkshop = reader.ReadBoolean();
		WorldX = reader.ReadInt16();
		WorldY = reader.ReadInt16();
		MapId = reader.ReadInt32();
		SubAreaId = reader.ReadVarUInt16();
		Status = Types.TypeFactory.CreateType<PlayerStatus>(reader.ReadUInt16());
		Status.Deserialize(reader);
	}
}
