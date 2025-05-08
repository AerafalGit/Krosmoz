// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Game.Guild.Tax;

namespace Krosmoz.Protocol.Messages.Game.Guild.Tax;

public sealed class TaxCollectorListMessage : DofusMessage
{
	public new const uint StaticProtocolId = 5930;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static TaxCollectorListMessage Empty =>
		new() { NbcollectorMax = 0, Informations = [], FightersInformations = [] };

	public required sbyte NbcollectorMax { get; set; }

	public required IEnumerable<TaxCollectorInformations> Informations { get; set; }

	public required IEnumerable<TaxCollectorFightersInformation> FightersInformations { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteSByte(NbcollectorMax);
		var informationsBefore = writer.Position;
		var informationsCount = 0;
		writer.WriteShort(0);
		foreach (var item in Informations)
		{
			writer.WriteUShort(item.ProtocolId);
			item.Serialize(writer);
			informationsCount++;
		}
		var informationsAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, informationsBefore);
		writer.WriteShort((short)informationsCount);
		writer.Seek(SeekOrigin.Begin, informationsAfter);
		var fightersInformationsBefore = writer.Position;
		var fightersInformationsCount = 0;
		writer.WriteShort(0);
		foreach (var item in FightersInformations)
		{
			item.Serialize(writer);
			fightersInformationsCount++;
		}
		var fightersInformationsAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, fightersInformationsBefore);
		writer.WriteShort((short)fightersInformationsCount);
		writer.Seek(SeekOrigin.Begin, fightersInformationsAfter);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		NbcollectorMax = reader.ReadSByte();
		var informationsCount = reader.ReadShort();
		var informations = new TaxCollectorInformations[informationsCount];
		for (var i = 0; i < informationsCount; i++)
		{
			var entry = Types.TypeFactory.CreateType<TaxCollectorInformations>(reader.ReadUShort());
			entry.Deserialize(reader);
			informations[i] = entry;
		}
		Informations = informations;
		var fightersInformationsCount = reader.ReadShort();
		var fightersInformations = new TaxCollectorFightersInformation[fightersInformationsCount];
		for (var i = 0; i < fightersInformationsCount; i++)
		{
			var entry = TaxCollectorFightersInformation.Empty;
			entry.Deserialize(reader);
			fightersInformations[i] = entry;
		}
		FightersInformations = fightersInformations;
	}
}
