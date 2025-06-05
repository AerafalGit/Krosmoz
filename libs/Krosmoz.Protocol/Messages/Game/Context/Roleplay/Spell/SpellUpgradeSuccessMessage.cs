// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Context.Roleplay.Spell;

public sealed class SpellUpgradeSuccessMessage : DofusMessage
{
	public new const uint StaticProtocolId = 1201;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static SpellUpgradeSuccessMessage Empty =>
		new() { SpellId = 0, SpellLevel = 0 };

	public required int SpellId { get; set; }

	public required sbyte SpellLevel { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteInt32(SpellId);
		writer.WriteInt8(SpellLevel);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		SpellId = reader.ReadInt32();
		SpellLevel = reader.ReadInt8();
	}
}
