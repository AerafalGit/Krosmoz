// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Game.Context.Fight;

namespace Krosmoz.Protocol.Messages.Game.Context.Roleplay;

public sealed class MapRunningFightDetailsMessage : DofusMessage
{
	public new const uint StaticProtocolId = 5751;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static MapRunningFightDetailsMessage Empty =>
		new() { FightId = 0, Attackers = [], Defenders = [] };

	public required int FightId { get; set; }

	public required IEnumerable<GameFightFighterLightInformations> Attackers { get; set; }

	public required IEnumerable<GameFightFighterLightInformations> Defenders { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteInt(FightId);
		var attackersBefore = writer.Position;
		var attackersCount = 0;
		writer.WriteShort(0);
		foreach (var item in Attackers)
		{
			item.Serialize(writer);
			attackersCount++;
		}
		var attackersAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, attackersBefore);
		writer.WriteShort((short)attackersCount);
		writer.Seek(SeekOrigin.Begin, attackersAfter);
		var defendersBefore = writer.Position;
		var defendersCount = 0;
		writer.WriteShort(0);
		foreach (var item in Defenders)
		{
			item.Serialize(writer);
			defendersCount++;
		}
		var defendersAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, defendersBefore);
		writer.WriteShort((short)defendersCount);
		writer.Seek(SeekOrigin.Begin, defendersAfter);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		FightId = reader.ReadInt();
		var attackersCount = reader.ReadShort();
		var attackers = new GameFightFighterLightInformations[attackersCount];
		for (var i = 0; i < attackersCount; i++)
		{
			var entry = GameFightFighterLightInformations.Empty;
			entry.Deserialize(reader);
			attackers[i] = entry;
		}
		Attackers = attackers;
		var defendersCount = reader.ReadShort();
		var defenders = new GameFightFighterLightInformations[defendersCount];
		for (var i = 0; i < defendersCount; i++)
		{
			var entry = GameFightFighterLightInformations.Empty;
			entry.Deserialize(reader);
			defenders[i] = entry;
		}
		Defenders = defenders;
	}
}
