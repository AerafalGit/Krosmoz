// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Types.Game.Context.Fight;

public sealed class GameFightFighterLightInformations : DofusType
{
	public new const ushort StaticProtocolId = 413;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static GameFightFighterLightInformations Empty =>
		new() { Sex = false, Alive = false, Id = 0, Name = string.Empty, Level = 0, Breed = 0 };

	public required bool Sex { get; set; }

	public required bool Alive { get; set; }

	public required int Id { get; set; }

	public required string Name { get; set; }

	public required short Level { get; set; }

	public required sbyte Breed { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		var flag = new byte();
		flag = BooleanByteWrapper.SetFlag(flag, 0, Sex);
		flag = BooleanByteWrapper.SetFlag(flag, 1, Alive);
		writer.WriteByte(flag);
		writer.WriteInt(Id);
		writer.WriteUtfLengthPrefixed16(Name);
		writer.WriteShort(Level);
		writer.WriteSByte(Breed);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		var flag = reader.ReadByte();
		Sex = BooleanByteWrapper.GetFlag(flag, 0);
		Alive = BooleanByteWrapper.GetFlag(flag, 1);
		Id = reader.ReadInt();
		Name = reader.ReadUtfLengthPrefixed16();
		Level = reader.ReadShort();
		Breed = reader.ReadSByte();
	}
}
