// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Types.Game.Interactive.Skill;

public sealed class SkillActionDescriptionCollect : SkillActionDescriptionTimed
{
	public new const ushort StaticProtocolId = 99;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static new SkillActionDescriptionCollect Empty =>
		new() { SkillId = 0, Time = 0, Min = 0, Max = 0 };

	public required short Min { get; set; }

	public required short Max { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		writer.WriteShort(Min);
		writer.WriteShort(Max);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		Min = reader.ReadShort();
		Max = reader.ReadShort();
	}
}
