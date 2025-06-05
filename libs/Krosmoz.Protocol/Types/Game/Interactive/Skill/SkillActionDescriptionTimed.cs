// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Types.Game.Interactive.Skill;

public class SkillActionDescriptionTimed : SkillActionDescription
{
	public new const ushort StaticProtocolId = 103;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static new SkillActionDescriptionTimed Empty =>
		new() { SkillId = 0, Time = 0 };

	public required byte Time { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		writer.WriteUInt8(Time);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		Time = reader.ReadUInt8();
	}
}
