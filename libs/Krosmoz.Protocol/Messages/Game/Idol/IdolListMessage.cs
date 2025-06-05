// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Game.Idol;

namespace Krosmoz.Protocol.Messages.Game.Idol;

public sealed class IdolListMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6585;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static IdolListMessage Empty =>
		new() { ChosenIdols = [], PartyChosenIdols = [], PartyIdols = [] };

	public required IEnumerable<ushort> ChosenIdols { get; set; }

	public required IEnumerable<ushort> PartyChosenIdols { get; set; }

	public required IEnumerable<PartyIdol> PartyIdols { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		var chosenIdolsBefore = writer.Position;
		var chosenIdolsCount = 0;
		writer.WriteInt16(0);
		foreach (var item in ChosenIdols)
		{
			writer.WriteVarUInt16(item);
			chosenIdolsCount++;
		}
		var chosenIdolsAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, chosenIdolsBefore);
		writer.WriteInt16((short)chosenIdolsCount);
		writer.Seek(SeekOrigin.Begin, chosenIdolsAfter);
		var partyChosenIdolsBefore = writer.Position;
		var partyChosenIdolsCount = 0;
		writer.WriteInt16(0);
		foreach (var item in PartyChosenIdols)
		{
			writer.WriteVarUInt16(item);
			partyChosenIdolsCount++;
		}
		var partyChosenIdolsAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, partyChosenIdolsBefore);
		writer.WriteInt16((short)partyChosenIdolsCount);
		writer.Seek(SeekOrigin.Begin, partyChosenIdolsAfter);
		var partyIdolsBefore = writer.Position;
		var partyIdolsCount = 0;
		writer.WriteInt16(0);
		foreach (var item in PartyIdols)
		{
			writer.WriteUInt16(item.ProtocolId);
			item.Serialize(writer);
			partyIdolsCount++;
		}
		var partyIdolsAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, partyIdolsBefore);
		writer.WriteInt16((short)partyIdolsCount);
		writer.Seek(SeekOrigin.Begin, partyIdolsAfter);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		var chosenIdolsCount = reader.ReadInt16();
		var chosenIdols = new ushort[chosenIdolsCount];
		for (var i = 0; i < chosenIdolsCount; i++)
		{
			chosenIdols[i] = reader.ReadVarUInt16();
		}
		ChosenIdols = chosenIdols;
		var partyChosenIdolsCount = reader.ReadInt16();
		var partyChosenIdols = new ushort[partyChosenIdolsCount];
		for (var i = 0; i < partyChosenIdolsCount; i++)
		{
			partyChosenIdols[i] = reader.ReadVarUInt16();
		}
		PartyChosenIdols = partyChosenIdols;
		var partyIdolsCount = reader.ReadInt16();
		var partyIdols = new PartyIdol[partyIdolsCount];
		for (var i = 0; i < partyIdolsCount; i++)
		{
			var entry = Types.TypeFactory.CreateType<PartyIdol>(reader.ReadUInt16());
			entry.Deserialize(reader);
			partyIdols[i] = entry;
		}
		PartyIdols = partyIdols;
	}
}
