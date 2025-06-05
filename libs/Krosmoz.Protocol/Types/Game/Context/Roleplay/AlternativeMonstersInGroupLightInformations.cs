// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Types.Game.Context.Roleplay;

public sealed class AlternativeMonstersInGroupLightInformations : DofusType
{
	public new const ushort StaticProtocolId = 394;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static AlternativeMonstersInGroupLightInformations Empty =>
		new() { PlayerCount = 0, Monsters = [] };

	public required int PlayerCount { get; set; }

	public required IEnumerable<MonsterInGroupLightInformations> Monsters { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteInt32(PlayerCount);
		var monstersBefore = writer.Position;
		var monstersCount = 0;
		writer.WriteInt16(0);
		foreach (var item in Monsters)
		{
			item.Serialize(writer);
			monstersCount++;
		}
		var monstersAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, monstersBefore);
		writer.WriteInt16((short)monstersCount);
		writer.Seek(SeekOrigin.Begin, monstersAfter);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		PlayerCount = reader.ReadInt32();
		var monstersCount = reader.ReadInt16();
		var monsters = new MonsterInGroupLightInformations[monstersCount];
		for (var i = 0; i < monstersCount; i++)
		{
			var entry = MonsterInGroupLightInformations.Empty;
			entry.Deserialize(reader);
			monsters[i] = entry;
		}
		Monsters = monsters;
	}
}
