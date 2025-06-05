// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Types.Game.Interactive.Skill;

public sealed class SkillActionDescriptionCraft : SkillActionDescription
{
	public new const ushort StaticProtocolId = 100;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static new SkillActionDescriptionCraft Empty =>
		new() { SkillId = 0, Probability = 0 };

	public required sbyte Probability { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		writer.WriteInt8(Probability);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		Probability = reader.ReadInt8();
	}
}
