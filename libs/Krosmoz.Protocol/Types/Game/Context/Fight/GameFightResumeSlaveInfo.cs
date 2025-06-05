// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Types.Game.Context.Fight;

public sealed class GameFightResumeSlaveInfo : DofusType
{
	public new const ushort StaticProtocolId = 364;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static GameFightResumeSlaveInfo Empty =>
		new() { SlaveId = 0, SpellCooldowns = [], SummonCount = 0, BombCount = 0 };

	public required int SlaveId { get; set; }

	public required IEnumerable<GameFightSpellCooldown> SpellCooldowns { get; set; }

	public required sbyte SummonCount { get; set; }

	public required sbyte BombCount { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteInt32(SlaveId);
		var spellCooldownsBefore = writer.Position;
		var spellCooldownsCount = 0;
		writer.WriteInt16(0);
		foreach (var item in SpellCooldowns)
		{
			item.Serialize(writer);
			spellCooldownsCount++;
		}
		var spellCooldownsAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, spellCooldownsBefore);
		writer.WriteInt16((short)spellCooldownsCount);
		writer.Seek(SeekOrigin.Begin, spellCooldownsAfter);
		writer.WriteInt8(SummonCount);
		writer.WriteInt8(BombCount);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		SlaveId = reader.ReadInt32();
		var spellCooldownsCount = reader.ReadInt16();
		var spellCooldowns = new GameFightSpellCooldown[spellCooldownsCount];
		for (var i = 0; i < spellCooldownsCount; i++)
		{
			var entry = GameFightSpellCooldown.Empty;
			entry.Deserialize(reader);
			spellCooldowns[i] = entry;
		}
		SpellCooldowns = spellCooldowns;
		SummonCount = reader.ReadInt8();
		BombCount = reader.ReadInt8();
	}
}
