// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Game.Character;
using Krosmoz.Protocol.Types.Game.Fight;

namespace Krosmoz.Protocol.Types.Game.Prism;

public sealed class PrismFightersInformation : DofusType
{
	public new const ushort StaticProtocolId = 443;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static PrismFightersInformation Empty =>
		new() { SubAreaId = 0, WaitingForHelpInfo = ProtectedEntityWaitingForHelpInfo.Empty, AllyCharactersInformations = [], EnemyCharactersInformations = [] };

	public required ushort SubAreaId { get; set; }

	public required ProtectedEntityWaitingForHelpInfo WaitingForHelpInfo { get; set; }

	public required IEnumerable<CharacterMinimalPlusLookInformations> AllyCharactersInformations { get; set; }

	public required IEnumerable<CharacterMinimalPlusLookInformations> EnemyCharactersInformations { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteVarUInt16(SubAreaId);
		WaitingForHelpInfo.Serialize(writer);
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
		SubAreaId = reader.ReadVarUInt16();
		WaitingForHelpInfo = ProtectedEntityWaitingForHelpInfo.Empty;
		WaitingForHelpInfo.Deserialize(reader);
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
