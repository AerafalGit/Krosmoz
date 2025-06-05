// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Types.Game.Interactive;

public class InteractiveElementSkill : DofusType
{
	public new const ushort StaticProtocolId = 219;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static InteractiveElementSkill Empty =>
		new() { SkillId = 0, SkillInstanceUid = 0 };

	public required uint SkillId { get; set; }

	public required int SkillInstanceUid { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteVarUInt32(SkillId);
		writer.WriteInt32(SkillInstanceUid);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		SkillId = reader.ReadVarUInt32();
		SkillInstanceUid = reader.ReadInt32();
	}
}
