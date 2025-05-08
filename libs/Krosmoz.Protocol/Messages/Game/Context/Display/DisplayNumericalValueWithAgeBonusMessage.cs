// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Context.Display;

public sealed class DisplayNumericalValueWithAgeBonusMessage : DisplayNumericalValueMessage
{
	public new const uint StaticProtocolId = 6361;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static new DisplayNumericalValueWithAgeBonusMessage Empty =>
		new() { Type = 0, Value = 0, EntityId = 0, ValueOfBonus = 0 };

	public required int ValueOfBonus { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		writer.WriteInt(ValueOfBonus);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		ValueOfBonus = reader.ReadInt();
	}
}
