// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Types.Game.Context.Roleplay;

public sealed class GroupMonsterStaticInformationsWithAlternatives : GroupMonsterStaticInformations
{
	public new const ushort StaticProtocolId = 396;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static new GroupMonsterStaticInformationsWithAlternatives Empty =>
		new() { Underlings = [], MainCreatureLightInfos = MonsterInGroupLightInformations.Empty, Alternatives = [] };

	public required IEnumerable<AlternativeMonstersInGroupLightInformations> Alternatives { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		var alternativesBefore = writer.Position;
		var alternativesCount = 0;
		writer.WriteInt16(0);
		foreach (var item in Alternatives)
		{
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
		var alternativesCount = reader.ReadInt16();
		var alternatives = new AlternativeMonstersInGroupLightInformations[alternativesCount];
		for (var i = 0; i < alternativesCount; i++)
		{
			var entry = AlternativeMonstersInGroupLightInformations.Empty;
			entry.Deserialize(reader);
			alternatives[i] = entry;
		}
		Alternatives = alternatives;
	}
}
