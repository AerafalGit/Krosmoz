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
		writer.WriteInt32(ElementId);
		writer.WriteInt32(ElementTypeId);
		var enabledSkillsBefore = writer.Position;
		var enabledSkillsCount = 0;
		writer.WriteInt16(0);
		foreach (var item in EnabledSkills)
		{
			writer.WriteUInt16(item.ProtocolId);
			item.Serialize(writer);
			enabledSkillsCount++;
		}
		var enabledSkillsAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, enabledSkillsBefore);
		writer.WriteInt16((short)enabledSkillsCount);
		writer.Seek(SeekOrigin.Begin, enabledSkillsAfter);
		var disabledSkillsBefore = writer.Position;
		var disabledSkillsCount = 0;
		writer.WriteInt16(0);
		foreach (var item in DisabledSkills)
		{
			writer.WriteUInt16(item.ProtocolId);
			item.Serialize(writer);
			disabledSkillsCount++;
		}
		var disabledSkillsAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, disabledSkillsBefore);
		writer.WriteInt16((short)disabledSkillsCount);
		writer.Seek(SeekOrigin.Begin, disabledSkillsAfter);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		ElementId = reader.ReadInt32();
		ElementTypeId = reader.ReadInt32();
		var enabledSkillsCount = reader.ReadInt16();
		var enabledSkills = new InteractiveElementSkill[enabledSkillsCount];
		for (var i = 0; i < enabledSkillsCount; i++)
		{
			var entry = Types.TypeFactory.CreateType<InteractiveElementSkill>(reader.ReadUInt16());
			entry.Deserialize(reader);
			enabledSkills[i] = entry;
		}
		EnabledSkills = enabledSkills;
		var disabledSkillsCount = reader.ReadInt16();
		var disabledSkills = new InteractiveElementSkill[disabledSkillsCount];
		for (var i = 0; i < disabledSkillsCount; i++)
		{
			var entry = Types.TypeFactory.CreateType<InteractiveElementSkill>(reader.ReadUInt16());
			entry.Deserialize(reader);
			disabledSkills[i] = entry;
		}
		DisabledSkills = disabledSkills;
	}
}
