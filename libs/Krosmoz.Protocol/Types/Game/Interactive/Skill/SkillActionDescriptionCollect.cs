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

	public required ushort Min { get; set; }

	public required ushort Max { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		writer.WriteVarUInt16(Min);
		writer.WriteVarUInt16(Max);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		Min = reader.ReadVarUInt16();
		Max = reader.ReadVarUInt16();
	}
}
