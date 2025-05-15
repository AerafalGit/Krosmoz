// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Game.Look;

namespace Krosmoz.Protocol.Types.Game.Context.Roleplay;

public sealed class GameRolePlayGroupMonsterInformations : GameRolePlayActorInformations
{
	public new const ushort StaticProtocolId = 160;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static new GameRolePlayGroupMonsterInformations Empty =>
		new() { Disposition = EntityDispositionInformations.Empty, Look = EntityLook.Empty, ContextualId = 0, KeyRingBonus = false, HasHardcoreDrop = false, StaticInfos = GroupMonsterStaticInformations.Empty, AgeBonus = 0, LootShare = 0, AlignmentSide = 0 };

	public required bool KeyRingBonus { get; set; }

	public required bool HasHardcoreDrop { get; set; }

	public required GroupMonsterStaticInformations StaticInfos { get; set; }

	public required short AgeBonus { get; set; }

	public required sbyte LootShare { get; set; }

	public required sbyte AlignmentSide { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		var flag = new byte();
		flag = BooleanByteWrapper.SetFlag(flag, 0, KeyRingBonus);
		flag = BooleanByteWrapper.SetFlag(flag, 1, HasHardcoreDrop);
		writer.WriteUInt8(flag);
		writer.WriteUInt16(StaticInfos.ProtocolId);
		StaticInfos.Serialize(writer);
		writer.WriteInt16(AgeBonus);
		writer.WriteInt8(LootShare);
		writer.WriteInt8(AlignmentSide);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		var flag = reader.ReadUInt8();
		KeyRingBonus = BooleanByteWrapper.GetFlag(flag, 0);
		HasHardcoreDrop = BooleanByteWrapper.GetFlag(flag, 1);
		StaticInfos = TypeFactory.CreateType<GroupMonsterStaticInformations>(reader.ReadUInt16());
		StaticInfos.Deserialize(reader);
		AgeBonus = reader.ReadInt16();
		LootShare = reader.ReadInt8();
		AlignmentSide = reader.ReadInt8();
	}
}
