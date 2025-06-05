// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Game.Guild.Tax;

namespace Krosmoz.Protocol.Messages.Game.Guild.Tax;

public class AbstractTaxCollectorListMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6568;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static AbstractTaxCollectorListMessage Empty =>
		new() { Informations = [] };

	public required IEnumerable<TaxCollectorInformations> Informations { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		var informationsBefore = writer.Position;
		var informationsCount = 0;
		writer.WriteInt16(0);
		foreach (var item in Informations)
		{
			writer.WriteUInt16(item.ProtocolId);
			item.Serialize(writer);
			informationsCount++;
		}
		var informationsAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, informationsBefore);
		writer.WriteInt16((short)informationsCount);
		writer.Seek(SeekOrigin.Begin, informationsAfter);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		var informationsCount = reader.ReadInt16();
		var informations = new TaxCollectorInformations[informationsCount];
		for (var i = 0; i < informationsCount; i++)
		{
			var entry = Types.TypeFactory.CreateType<TaxCollectorInformations>(reader.ReadUInt16());
			entry.Deserialize(reader);
			informations[i] = entry;
		}
		Informations = informations;
	}
}
