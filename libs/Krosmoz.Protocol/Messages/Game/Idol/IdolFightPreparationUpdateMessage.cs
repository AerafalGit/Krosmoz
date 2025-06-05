// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Idol;

public sealed class IdolFightPreparationUpdateMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6586;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static IdolFightPreparationUpdateMessage Empty =>
		new() { IdolSource = 0, Idols = [] };

	public required sbyte IdolSource { get; set; }

	public required IEnumerable<Types.Game.Idol.Idol> Idols { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteInt8(IdolSource);
		var idolsBefore = writer.Position;
		var idolsCount = 0;
		writer.WriteInt16(0);
		foreach (var item in Idols)
		{
			writer.WriteUInt16(item.ProtocolId);
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
		IdolSource = reader.ReadInt8();
		var idolsCount = reader.ReadInt16();
		var idols = new Types.Game.Idol.Idol[idolsCount];
		for (var i = 0; i < idolsCount; i++)
		{
			var entry = Types.TypeFactory.CreateType<Types.Game.Idol.Idol>(reader.ReadUInt16());
			entry.Deserialize(reader);
			idols[i] = entry;
		}
		Idols = idols;
	}
}
