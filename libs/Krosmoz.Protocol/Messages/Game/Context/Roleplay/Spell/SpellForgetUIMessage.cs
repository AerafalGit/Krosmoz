// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Context.Roleplay.Spell;

public sealed class SpellForgetUIMessage : DofusMessage
{
	public new const uint StaticProtocolId = 5565;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static SpellForgetUIMessage Empty =>
		new() { Open = false };

	public required bool Open { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteBoolean(Open);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		Open = reader.ReadBoolean();
	}
}
