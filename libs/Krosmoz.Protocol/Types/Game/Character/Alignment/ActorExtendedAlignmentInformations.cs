// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Types.Game.Character.Alignment;

public sealed class ActorExtendedAlignmentInformations : ActorAlignmentInformations
{
	public new const ushort StaticProtocolId = 202;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static new ActorExtendedAlignmentInformations Empty =>
		new() { CharacterPower = 0, AlignmentGrade = 0, AlignmentValue = 0, AlignmentSide = 0, Honor = 0, HonorGradeFloor = 0, HonorNextGradeFloor = 0, Aggressable = 0 };

	public required ushort Honor { get; set; }

	public required ushort HonorGradeFloor { get; set; }

	public required ushort HonorNextGradeFloor { get; set; }

	public required sbyte Aggressable { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		writer.WriteVarUInt16(Honor);
		writer.WriteVarUInt16(HonorGradeFloor);
		writer.WriteVarUInt16(HonorNextGradeFloor);
		writer.WriteInt8(Aggressable);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		Honor = reader.ReadVarUInt16();
		HonorGradeFloor = reader.ReadVarUInt16();
		HonorNextGradeFloor = reader.ReadVarUInt16();
		Aggressable = reader.ReadInt8();
	}
}
