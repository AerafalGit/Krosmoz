// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Types.Game.Context.Fight;

public sealed class FightResultPlayerListEntry : FightResultFighterListEntry
{
	public new const ushort StaticProtocolId = 24;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static new FightResultPlayerListEntry Empty =>
		new() { Rewards = FightLoot.Empty, Wave = 0, Outcome = 0, Alive = false, Id = 0, Level = 0, Additional = [] };

	public required byte Level { get; set; }

	public required IEnumerable<FightResultAdditionalData> Additional { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		writer.WriteUInt8(Level);
		var additionalBefore = writer.Position;
		var additionalCount = 0;
		writer.WriteInt16(0);
		foreach (var item in Additional)
		{
			writer.WriteUInt16(item.ProtocolId);
			item.Serialize(writer);
			additionalCount++;
		}
		var additionalAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, additionalBefore);
		writer.WriteInt16((short)additionalCount);
		writer.Seek(SeekOrigin.Begin, additionalAfter);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		Level = reader.ReadUInt8();
		var additionalCount = reader.ReadInt16();
		var additional = new FightResultAdditionalData[additionalCount];
		for (var i = 0; i < additionalCount; i++)
		{
			var entry = Types.TypeFactory.CreateType<FightResultAdditionalData>(reader.ReadUInt16());
			entry.Deserialize(reader);
			additional[i] = entry;
		}
		Additional = additional;
	}
}
