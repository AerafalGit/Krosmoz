// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Game.House;

namespace Krosmoz.Protocol.Messages.Game.Guild;

public sealed class GuildHousesInformationMessage : DofusMessage
{
	public new const uint StaticProtocolId = 5919;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static GuildHousesInformationMessage Empty =>
		new() { HousesInformations = [] };

	public required IEnumerable<HouseInformationsForGuild> HousesInformations { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		var housesInformationsBefore = writer.Position;
		var housesInformationsCount = 0;
		writer.WriteInt16(0);
		foreach (var item in HousesInformations)
		{
			item.Serialize(writer);
			housesInformationsCount++;
		}
		var housesInformationsAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, housesInformationsBefore);
		writer.WriteInt16((short)housesInformationsCount);
		writer.Seek(SeekOrigin.Begin, housesInformationsAfter);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		var housesInformationsCount = reader.ReadInt16();
		var housesInformations = new HouseInformationsForGuild[housesInformationsCount];
		for (var i = 0; i < housesInformationsCount; i++)
		{
			var entry = HouseInformationsForGuild.Empty;
			entry.Deserialize(reader);
			housesInformations[i] = entry;
		}
		HousesInformations = housesInformations;
	}
}
