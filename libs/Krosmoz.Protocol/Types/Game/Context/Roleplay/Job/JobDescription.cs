// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Game.Interactive.Skill;

namespace Krosmoz.Protocol.Types.Game.Context.Roleplay.Job;

public sealed class JobDescription : DofusType
{
	public new const ushort StaticProtocolId = 101;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static JobDescription Empty =>
		new() { JobId = 0, Skills = [] };

	public required sbyte JobId { get; set; }

	public required IEnumerable<SkillActionDescription> Skills { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteInt8(JobId);
		var skillsBefore = writer.Position;
		var skillsCount = 0;
		writer.WriteInt16(0);
		foreach (var item in Skills)
		{
			writer.WriteUInt16(item.ProtocolId);
			item.Serialize(writer);
			skillsCount++;
		}
		var skillsAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, skillsBefore);
		writer.WriteInt16((short)skillsCount);
		writer.Seek(SeekOrigin.Begin, skillsAfter);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		JobId = reader.ReadInt8();
		var skillsCount = reader.ReadInt16();
		var skills = new SkillActionDescription[skillsCount];
		for (var i = 0; i < skillsCount; i++)
		{
			var entry = Types.TypeFactory.CreateType<SkillActionDescription>(reader.ReadUInt16());
			entry.Deserialize(reader);
			skills[i] = entry;
		}
		Skills = skills;
	}
}
