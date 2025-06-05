// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Game.Guild.Tax;

namespace Krosmoz.Protocol.Messages.Game.Guild.Tax;

public sealed class TaxCollectorListMessage : AbstractTaxCollectorListMessage
{
	public new const uint StaticProtocolId = 5930;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static new TaxCollectorListMessage Empty =>
		new() { Informations = [], NbcollectorMax = 0, FightersInformations = [] };

	public required sbyte NbcollectorMax { get; set; }

	public required IEnumerable<TaxCollectorFightersInformation> FightersInformations { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		writer.WriteInt8(NbcollectorMax);
		var fightersInformationsBefore = writer.Position;
		var fightersInformationsCount = 0;
		writer.WriteInt16(0);
		foreach (var item in FightersInformations)
		{
			item.Serialize(writer);
			fightersInformationsCount++;
		}
		var fightersInformationsAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, fightersInformationsBefore);
		writer.WriteInt16((short)fightersInformationsCount);
		writer.Seek(SeekOrigin.Begin, fightersInformationsAfter);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		NbcollectorMax = reader.ReadInt8();
		var fightersInformationsCount = reader.ReadInt16();
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
