// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Types.Game.Interactive;

public sealed class InteractiveElementWithAgeBonus : InteractiveElement
{
	public new const ushort StaticProtocolId = 398;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static new InteractiveElementWithAgeBonus Empty =>
		new() { DisabledSkills = [], EnabledSkills = [], ElementTypeId = 0, ElementId = 0, AgeBonus = 0 };

	public required short AgeBonus { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		writer.WriteInt16(AgeBonus);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		AgeBonus = reader.ReadInt16();
	}
}
