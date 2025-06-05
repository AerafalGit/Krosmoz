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

	public required ushort SpellId { get; set; }

	public required CharacterBaseCharacteristic Value { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteInt8(ModificationType);
		writer.WriteVarUInt16(SpellId);
		Value.Serialize(writer);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		ModificationType = reader.ReadInt8();
		SpellId = reader.ReadVarUInt16();
		Value = CharacterBaseCharacteristic.Empty;
		Value.Deserialize(reader);
	}
}
