// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Types.Game.Interactive;

public sealed class InteractiveElementNamedSkill : InteractiveElementSkill
{
	public new const ushort StaticProtocolId = 220;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static new InteractiveElementNamedSkill Empty =>
		new() { SkillInstanceUid = 0, SkillId = 0, NameId = 0 };

	public required uint NameId { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		writer.WriteVarUInt32(NameId);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		NameId = reader.ReadVarUInt32();
	}
}
