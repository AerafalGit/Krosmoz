// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Types.Game.Character.Characteristic;

public sealed class CharacterSpellModification : DofusType
{
	public new const ushort StaticProtocolId = 215;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static CharacterSpellModification Empty =>
		new() { ModificationType = 0, SpellId = 0, Value = CharacterBaseCharacteristic.Empty };

	public required sbyte ModificationType { get; set; }

	public required short SpellId { get; set; }

	public required CharacterBaseCharacteristic Value { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteSByte(ModificationType);
		writer.WriteShort(SpellId);
		Value.Serialize(writer);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		ModificationType = reader.ReadSByte();
		SpellId = reader.ReadShort();
		Value = CharacterBaseCharacteristic.Empty;
		Value.Deserialize(reader);
	}
}
