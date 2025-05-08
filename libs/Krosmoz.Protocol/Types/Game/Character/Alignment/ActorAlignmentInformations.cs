// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Types.Game.Character.Alignment;

public class ActorAlignmentInformations : DofusType
{
	public new const ushort StaticProtocolId = 201;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static ActorAlignmentInformations Empty =>
		new() { AlignmentSide = 0, AlignmentValue = 0, AlignmentGrade = 0, CharacterPower = 0 };

	public required sbyte AlignmentSide { get; set; }

	public required sbyte AlignmentValue { get; set; }

	public required sbyte AlignmentGrade { get; set; }

	public required int CharacterPower { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteSByte(AlignmentSide);
		writer.WriteSByte(AlignmentValue);
		writer.WriteSByte(AlignmentGrade);
		writer.WriteInt(CharacterPower);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		AlignmentSide = reader.ReadSByte();
		AlignmentValue = reader.ReadSByte();
		AlignmentGrade = reader.ReadSByte();
		CharacterPower = reader.ReadInt();
	}
}
