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
		writer.WriteInt(SlaveId);
		var spellCooldownsBefore = writer.Position;
		var spellCooldownsCount = 0;
		writer.WriteShort(0);
		foreach (var item in SpellCooldowns)
		{
			item.Serialize(writer);
			spellCooldownsCount++;
		}
		var spellCooldownsAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, spellCooldownsBefore);
		writer.WriteShort((short)spellCooldownsCount);
		writer.Seek(SeekOrigin.Begin, spellCooldownsAfter);
		writer.WriteSByte(SummonCount);
		writer.WriteSByte(BombCount);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		SlaveId = reader.ReadInt();
		var spellCooldownsCount = reader.ReadShort();
		var spellCooldowns = new GameFightSpellCooldown[spellCooldownsCount];
		for (var i = 0; i < spellCooldownsCount; i++)
		{
			var entry = GameFightSpellCooldown.Empty;
			entry.Deserialize(reader);
			spellCooldowns[i] = entry;
		}
		SpellCooldowns = spellCooldowns;
		SummonCount = reader.ReadSByte();
		BombCount = reader.ReadSByte();
	}
}
