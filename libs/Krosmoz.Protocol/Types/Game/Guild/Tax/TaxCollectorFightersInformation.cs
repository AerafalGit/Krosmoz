// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Game.Character;

namespace Krosmoz.Protocol.Types.Game.Guild.Tax;

public sealed class TaxCollectorFightersInformation : DofusType
{
	public new const ushort StaticProtocolId = 169;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static TaxCollectorFightersInformation Empty =>
		new() { CollectorId = 0, AllyCharactersInformations = [], EnemyCharactersInformations = [] };

	public required int CollectorId { get; set; }

	public required IEnumerable<CharacterMinimalPlusLookInformations> AllyCharactersInformations { get; set; }

	public required IEnumerable<CharacterMinimalPlusLookInformations> EnemyCharactersInformations { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteInt32(CollectorId);
		var allyCharactersInformationsBefore = writer.Position;
		var allyCharactersInformationsCount = 0;
		writer.WriteInt16(0);
		foreach (var item in AllyCharactersInformations)
		{
			writer.WriteUInt16(item.ProtocolId);
			item.Serialize(writer);
			allyCharactersInformationsCount++;
		}
		var allyCharactersInformationsAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, allyCharactersInformationsBefore);
		writer.WriteInt16((short)allyCharactersInformationsCount);
		writer.Seek(SeekOrigin.Begin, allyCharactersInformationsAfter);
		var enemyCharactersInformationsBefore = writer.Position;
		var enemyCharactersInformationsCount = 0;
		writer.WriteInt16(0);
		foreach (var item in EnemyCharactersInformations)
		{
			writer.WriteUInt16(item.ProtocolId);
			item.Serialize(writer);
			enemyCharactersInformationsCount++;
		}
		var enemyCharactersInformationsAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, enemyCharactersInformationsBefore);
		writer.WriteInt16((short)enemyCharactersInformationsCount);
		writer.Seek(SeekOrigin.Begin, enemyCharactersInformationsAfter);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		CollectorId = reader.ReadInt32();
		var allyCharactersInformationsCount = reader.ReadInt16();
		var allyCharactersInformations = new CharacterMinimalPlusLookInformations[allyCharactersInformationsCount];
		for (var i = 0; i < allyCharactersInformationsCount; i++)
		{
			var entry = Types.TypeFactory.CreateType<CharacterMinimalPlusLookInformations>(reader.ReadUInt16());
			entry.Deserialize(reader);
			allyCharactersInformations[i] = entry;
		}
		AllyCharactersInformations = allyCharactersInformations;
		var enemyCharactersInformationsCount = reader.ReadInt16();
		var enemyCharactersInformations = new CharacterMinimalPlusLookInformations[enemyCharactersInformationsCount];
		for (var i = 0; i < enemyCharactersInformationsCount; i++)
		{
			var entry = Types.TypeFactory.CreateType<CharacterMinimalPlusLookInformations>(reader.ReadUInt16());
			entry.Deserialize(reader);
			enemyCharactersInformations[i] = entry;
		}
		EnemyCharactersInformations = enemyCharactersInformations;
	}
}
