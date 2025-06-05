// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Types.Game.Context.Fight;

public sealed class GameFightSpellCooldown : DofusType
{
	public new const ushort StaticProtocolId = 205;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static GameFightSpellCooldown Empty =>
		new() { SpellId = 0, Cooldown = 0 };

	public required int SpellId { get; set; }

	public required sbyte Cooldown { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteInt32(SpellId);
		writer.WriteInt8(Cooldown);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		SpellId = reader.ReadInt32();
		Cooldown = reader.ReadInt8();
	}
}
