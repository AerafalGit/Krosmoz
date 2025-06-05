// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Game.Look;

namespace Krosmoz.Protocol.Types.Game.Context.Roleplay;

public sealed class GameRolePlayGroupMonsterWaveInformations : GameRolePlayGroupMonsterInformations
{
	public new const ushort StaticProtocolId = 464;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static new GameRolePlayGroupMonsterWaveInformations Empty =>
		new() { Disposition = EntityDispositionInformations.Empty, Look = EntityLook.Empty, ContextualId = 0, AlignmentSide = 0, LootShare = 0, AgeBonus = 0, StaticInfos = GroupMonsterStaticInformations.Empty, HasAVARewardToken = false, HasHardcoreDrop = false, KeyRingBonus = false, NbWaves = 0, Alternatives = [] };

	public required sbyte NbWaves { get; set; }

	public required IEnumerable<GroupMonsterStaticInformations> Alternatives { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		writer.WriteInt8(NbWaves);
		var alternativesBefore = writer.Position;
		var alternativesCount = 0;
		writer.WriteInt16(0);
		foreach (var item in Alternatives)
		{
			writer.WriteUInt16(item.ProtocolId);
			item.Serialize(writer);
			alternativesCount++;
		}
		var alternativesAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, alternativesBefore);
		writer.WriteInt16((short)alternativesCount);
		writer.Seek(SeekOrigin.Begin, alternativesAfter);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		NbWaves = reader.ReadInt8();
		var alternativesCount = reader.ReadInt16();
		var alternatives = new GroupMonsterStaticInformations[alternativesCount];
		for (var i = 0; i < alternativesCount; i++)
		{
			var entry = Types.TypeFactory.CreateType<GroupMonsterStaticInformations>(reader.ReadUInt16());
			entry.Deserialize(reader);
			alternatives[i] = entry;
		}
		Alternatives = alternatives;
	}
}
