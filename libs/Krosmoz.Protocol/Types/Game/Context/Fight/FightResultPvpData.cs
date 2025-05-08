// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Types.Game.Context.Fight;

public sealed class FightResultPvpData : FightResultAdditionalData
{
	public new const ushort StaticProtocolId = 190;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static new FightResultPvpData Empty =>
		new() { Grade = 0, MinHonorForGrade = 0, MaxHonorForGrade = 0, Honor = 0, HonorDelta = 0 };

	public required byte Grade { get; set; }

	public required ushort MinHonorForGrade { get; set; }

	public required ushort MaxHonorForGrade { get; set; }

	public required ushort Honor { get; set; }

	public required short HonorDelta { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		writer.WriteByte(Grade);
		writer.WriteUShort(MinHonorForGrade);
		writer.WriteUShort(MaxHonorForGrade);
		writer.WriteUShort(Honor);
		writer.WriteShort(HonorDelta);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		Grade = reader.ReadByte();
		MinHonorForGrade = reader.ReadUShort();
		MaxHonorForGrade = reader.ReadUShort();
		Honor = reader.ReadUShort();
		HonorDelta = reader.ReadShort();
	}
}
