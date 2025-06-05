// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Context.Fight;

public sealed class GameFightStartMessage : DofusMessage
{
	public new const uint StaticProtocolId = 712;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static GameFightStartMessage Empty =>
		new() { Idols = [] };

	public required IEnumerable<Types.Game.Idol.Idol> Idols { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		var idolsBefore = writer.Position;
		var idolsCount = 0;
		writer.WriteInt16(0);
		foreach (var item in Idols)
		{
			item.Serialize(writer);
			idolsCount++;
		}
		var idolsAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, idolsBefore);
		writer.WriteInt16((short)idolsCount);
		writer.Seek(SeekOrigin.Begin, idolsAfter);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		var idolsCount = reader.ReadInt16();
		var idols = new Types.Game.Idol.Idol[idolsCount];
		for (var i = 0; i < idolsCount; i++)
		{
			var entry = Types.Game.Idol.Idol.Empty;
			entry.Deserialize(reader);
			idols[i] = entry;
		}
		Idols = idols;
	}
}
