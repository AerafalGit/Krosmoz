// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Types.Game.Context.Roleplay;

public class GroupMonsterStaticInformations : DofusType
{
	public new const ushort StaticProtocolId = 140;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static GroupMonsterStaticInformations Empty =>
		new() { MainCreatureLightInfos = MonsterInGroupLightInformations.Empty, Underlings = [] };

	public required MonsterInGroupLightInformations MainCreatureLightInfos { get; set; }

	public required IEnumerable<MonsterInGroupInformations> Underlings { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		MainCreatureLightInfos.Serialize(writer);
		var underlingsBefore = writer.Position;
		var underlingsCount = 0;
		writer.WriteInt16(0);
		foreach (var item in Underlings)
		{
			item.Serialize(writer);
			underlingsCount++;
		}
		var underlingsAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, underlingsBefore);
		writer.WriteInt16((short)underlingsCount);
		writer.Seek(SeekOrigin.Begin, underlingsAfter);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		MainCreatureLightInfos = MonsterInGroupLightInformations.Empty;
		MainCreatureLightInfos.Deserialize(reader);
		var underlingsCount = reader.ReadInt16();
		var underlings = new MonsterInGroupInformations[underlingsCount];
		for (var i = 0; i < underlingsCount; i++)
		{
			var entry = MonsterInGroupInformations.Empty;
			entry.Deserialize(reader);
			underlings[i] = entry;
		}
		Underlings = underlings;
	}
}
