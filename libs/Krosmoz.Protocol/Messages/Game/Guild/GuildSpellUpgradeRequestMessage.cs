// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Guild;

public sealed class GuildSpellUpgradeRequestMessage : DofusMessage
{
	public new const uint StaticProtocolId = 5699;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static GuildSpellUpgradeRequestMessage Empty =>
		new() { SpellId = 0 };

	public required int SpellId { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteInt32(SpellId);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		SpellId = reader.ReadInt32();
	}
}
