// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Context.Roleplay.Job;

public sealed class JobMultiCraftAvailableSkillsMessage : JobAllowMultiCraftRequestMessage
{
	public new const uint StaticProtocolId = 5747;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static new JobMultiCraftAvailableSkillsMessage Empty =>
		new() { Enabled = false, PlayerId = 0, Skills = [] };

	public required uint PlayerId { get; set; }

	public required IEnumerable<ushort> Skills { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		writer.WriteVarUInt32(PlayerId);
		var skillsBefore = writer.Position;
		var skillsCount = 0;
		writer.WriteInt16(0);
		foreach (var item in Skills)
		{
			writer.WriteVarUInt16(item);
			skillsCount++;
		}
		var skillsAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, skillsBefore);
		writer.WriteInt16((short)skillsCount);
		writer.Seek(SeekOrigin.Begin, skillsAfter);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		PlayerId = reader.ReadVarUInt32();
		var skillsCount = reader.ReadInt16();
		var skills = new ushort[skillsCount];
		for (var i = 0; i < skillsCount; i++)
		{
			skills[i] = reader.ReadVarUInt16();
		}
		Skills = skills;
	}
}
