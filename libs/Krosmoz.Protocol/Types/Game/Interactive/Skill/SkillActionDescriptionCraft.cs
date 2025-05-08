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
		new() { SkillId = 0, MaxSlots = 0, Probability = 0 };

	public required sbyte MaxSlots { get; set; }

	public required sbyte Probability { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		writer.WriteSByte(MaxSlots);
		writer.WriteSByte(Probability);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		MaxSlots = reader.ReadSByte();
		Probability = reader.ReadSByte();
	}
}
