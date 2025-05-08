// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Game.Paddock;

namespace Krosmoz.Protocol.Messages.Game.Guild;

public sealed class GuildInformationsPaddocksMessage : DofusMessage
{
	public new const uint StaticProtocolId = 5959;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static GuildInformationsPaddocksMessage Empty =>
		new() { NbPaddockMax = 0, PaddocksInformations = [] };

	public required sbyte NbPaddockMax { get; set; }

	public required IEnumerable<PaddockContentInformations> PaddocksInformations { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteSByte(NbPaddockMax);
		var paddocksInformationsBefore = writer.Position;
		var paddocksInformationsCount = 0;
		writer.WriteShort(0);
		foreach (var item in PaddocksInformations)
		{
			item.Serialize(writer);
			paddocksInformationsCount++;
		}
		var paddocksInformationsAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, paddocksInformationsBefore);
		writer.WriteShort((short)paddocksInformationsCount);
		writer.Seek(SeekOrigin.Begin, paddocksInformationsAfter);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		NbPaddockMax = reader.ReadSByte();
		var paddocksInformationsCount = reader.ReadShort();
		var paddocksInformations = new PaddockContentInformations[paddocksInformationsCount];
		for (var i = 0; i < paddocksInformationsCount; i++)
		{
			var entry = PaddockContentInformations.Empty;
			entry.Deserialize(reader);
			paddocksInformations[i] = entry;
		}
		PaddocksInformations = paddocksInformations;
	}
}
