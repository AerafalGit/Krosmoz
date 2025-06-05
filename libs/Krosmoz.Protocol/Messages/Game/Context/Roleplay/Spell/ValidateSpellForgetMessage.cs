// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Context.Roleplay.Spell;

public sealed class ValidateSpellForgetMessage : DofusMessage
{
	public new const uint StaticProtocolId = 1700;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static ValidateSpellForgetMessage Empty =>
		new() { SpellId = 0 };

	public required ushort SpellId { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteVarUInt16(SpellId);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		SpellId = reader.ReadVarUInt16();
	}
}
