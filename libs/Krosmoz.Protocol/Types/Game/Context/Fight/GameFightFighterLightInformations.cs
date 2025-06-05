// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Types.Game.Context.Fight;

public class GameFightFighterLightInformations : DofusType
{
	public new const ushort StaticProtocolId = 413;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static GameFightFighterLightInformations Empty =>
		new() { Sex = false, Alive = false, Id = 0, Wave = 0, Level = 0, Breed = 0 };

	public required bool Sex { get; set; }

	public required bool Alive { get; set; }

	public required int Id { get; set; }

	public required sbyte Wave { get; set; }

	public required ushort Level { get; set; }

	public required sbyte Breed { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		var flag = new byte();
		flag = BooleanByteWrapper.SetFlag(flag, 0, Sex);
		flag = BooleanByteWrapper.SetFlag(flag, 1, Alive);
		writer.WriteUInt8(flag);
		writer.WriteInt32(Id);
		writer.WriteInt8(Wave);
		writer.WriteVarUInt16(Level);
		writer.WriteInt8(Breed);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		var flag = reader.ReadUInt8();
		Sex = BooleanByteWrapper.GetFlag(flag, 0);
		Alive = BooleanByteWrapper.GetFlag(flag, 1);
		Id = reader.ReadInt32();
		Wave = reader.ReadInt8();
		Level = reader.ReadVarUInt16();
		Breed = reader.ReadInt8();
	}
}
