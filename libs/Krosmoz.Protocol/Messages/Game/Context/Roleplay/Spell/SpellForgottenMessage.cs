// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Context.Roleplay.Spell;

public sealed class SpellForgottenMessage : DofusMessage
{
	public new const uint StaticProtocolId = 5834;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static SpellForgottenMessage Empty =>
		new() { SpellsId = [], BoostPoint = 0 };

	public required IEnumerable<ushort> SpellsId { get; set; }

	public required ushort BoostPoint { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		var spellsIdBefore = writer.Position;
		var spellsIdCount = 0;
		writer.WriteInt16(0);
		foreach (var item in SpellsId)
		{
			writer.WriteVarUInt16(item);
			spellsIdCount++;
		}
		var spellsIdAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, spellsIdBefore);
		writer.WriteInt16((short)spellsIdCount);
		writer.Seek(SeekOrigin.Begin, spellsIdAfter);
		writer.WriteVarUInt16(BoostPoint);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		var spellsIdCount = reader.ReadInt16();
		var spellsId = new ushort[spellsIdCount];
		for (var i = 0; i < spellsIdCount; i++)
		{
			spellsId[i] = reader.ReadVarUInt16();
		}
		SpellsId = spellsId;
		BoostPoint = reader.ReadVarUInt16();
	}
}
