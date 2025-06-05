// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Context.Roleplay.TreasureHunt;

public sealed class TreasureHuntShowLegendaryUIMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6498;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static TreasureHuntShowLegendaryUIMessage Empty =>
		new() { AvailableLegendaryIds = [] };

	public required IEnumerable<ushort> AvailableLegendaryIds { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		var availableLegendaryIdsBefore = writer.Position;
		var availableLegendaryIdsCount = 0;
		writer.WriteInt16(0);
		foreach (var item in AvailableLegendaryIds)
		{
			writer.WriteVarUInt16(item);
			availableLegendaryIdsCount++;
		}
		var availableLegendaryIdsAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, availableLegendaryIdsBefore);
		writer.WriteInt16((short)availableLegendaryIdsCount);
		writer.Seek(SeekOrigin.Begin, availableLegendaryIdsAfter);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		var availableLegendaryIdsCount = reader.ReadInt16();
		var availableLegendaryIds = new ushort[availableLegendaryIdsCount];
		for (var i = 0; i < availableLegendaryIdsCount; i++)
		{
			availableLegendaryIds[i] = reader.ReadVarUInt16();
		}
		AvailableLegendaryIds = availableLegendaryIds;
	}
}
