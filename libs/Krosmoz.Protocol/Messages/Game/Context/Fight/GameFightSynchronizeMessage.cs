// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Game.Context.Fight;

namespace Krosmoz.Protocol.Messages.Game.Context.Fight;

public sealed class GameFightSynchronizeMessage : DofusMessage
{
	public new const uint StaticProtocolId = 5921;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static GameFightSynchronizeMessage Empty =>
		new() { Fighters = [] };

	public required IEnumerable<GameFightFighterInformations> Fighters { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		var fightersBefore = writer.Position;
		var fightersCount = 0;
		writer.WriteInt16(0);
		foreach (var item in Fighters)
		{
			writer.WriteUInt16(item.ProtocolId);
			item.Serialize(writer);
			fightersCount++;
		}
		var fightersAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, fightersBefore);
		writer.WriteInt16((short)fightersCount);
		writer.Seek(SeekOrigin.Begin, fightersAfter);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		var fightersCount = reader.ReadInt16();
		var fighters = new GameFightFighterInformations[fightersCount];
		for (var i = 0; i < fightersCount; i++)
		{
			var entry = Types.TypeFactory.CreateType<GameFightFighterInformations>(reader.ReadUInt16());
			entry.Deserialize(reader);
			fighters[i] = entry;
		}
		Fighters = fighters;
	}
}
