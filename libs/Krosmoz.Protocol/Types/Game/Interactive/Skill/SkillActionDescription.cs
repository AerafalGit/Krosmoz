// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Types.Game.Interactive.Skill;

public class SkillActionDescription : DofusType
{
	public new const ushort StaticProtocolId = 102;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static SkillActionDescription Empty =>
		new() { SkillId = 0 };

	public required ushort SkillId { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteVarUInt16(SkillId);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		SkillId = reader.ReadVarUInt16();
	}
}
