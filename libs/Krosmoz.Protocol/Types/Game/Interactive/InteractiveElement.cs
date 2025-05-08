// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Types.Game.Interactive;

public class InteractiveElement : DofusType
{
	public new const ushort StaticProtocolId = 80;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static InteractiveElement Empty =>
		new() { ElementId = 0, ElementTypeId = 0, EnabledSkills = [], DisabledSkills = [] };

	public required int ElementId { get; set; }

	public required int ElementTypeId { get; set; }

	public required IEnumerable<InteractiveElementSkill> EnabledSkills { get; set; }

	public required IEnumerable<InteractiveElementSkill> DisabledSkills { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteInt(ElementId);
		writer.WriteInt(ElementTypeId);
		var enabledSkillsBefore = writer.Position;
		var enabledSkillsCount = 0;
		writer.WriteShort(0);
		foreach (var item in EnabledSkills)
		{
			writer.WriteUShort(item.ProtocolId);
			item.Serialize(writer);
			enabledSkillsCount++;
		}
		var enabledSkillsAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, enabledSkillsBefore);
		writer.WriteShort((short)enabledSkillsCount);
		writer.Seek(SeekOrigin.Begin, enabledSkillsAfter);
		var disabledSkillsBefore = writer.Position;
		var disabledSkillsCount = 0;
		writer.WriteShort(0);
		foreach (var item in DisabledSkills)
		{
			writer.WriteUShort(item.ProtocolId);
			item.Serialize(writer);
			disabledSkillsCount++;
		}
		var disabledSkillsAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, disabledSkillsBefore);
		writer.WriteShort((short)disabledSkillsCount);
		writer.Seek(SeekOrigin.Begin, disabledSkillsAfter);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		ElementId = reader.ReadInt();
		ElementTypeId = reader.ReadInt();
		var enabledSkillsCount = reader.ReadShort();
		var enabledSkills = new InteractiveElementSkill[enabledSkillsCount];
		for (var i = 0; i < enabledSkillsCount; i++)
		{
			var entry = Types.TypeFactory.CreateType<InteractiveElementSkill>(reader.ReadUShort());
			entry.Deserialize(reader);
			enabledSkills[i] = entry;
		}
		EnabledSkills = enabledSkills;
		var disabledSkillsCount = reader.ReadShort();
		var disabledSkills = new InteractiveElementSkill[disabledSkillsCount];
		for (var i = 0; i < disabledSkillsCount; i++)
		{
			var entry = Types.TypeFactory.CreateType<InteractiveElementSkill>(reader.ReadUShort());
			entry.Deserialize(reader);
			disabledSkills[i] = entry;
		}
		DisabledSkills = disabledSkills;
	}
}
