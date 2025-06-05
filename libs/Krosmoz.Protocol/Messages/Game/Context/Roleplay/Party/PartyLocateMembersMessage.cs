// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Game.Context.Roleplay.Party;

namespace Krosmoz.Protocol.Messages.Game.Context.Roleplay.Party;

public sealed class PartyLocateMembersMessage : AbstractPartyMessage
{
	public new const uint StaticProtocolId = 5595;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static new PartyLocateMembersMessage Empty =>
		new() { PartyId = 0, Geopositions = [] };

	public required IEnumerable<PartyMemberGeoPosition> Geopositions { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		var geopositionsBefore = writer.Position;
		var geopositionsCount = 0;
		writer.WriteInt16(0);
		foreach (var item in Geopositions)
		{
			item.Serialize(writer);
			geopositionsCount++;
		}
		var geopositionsAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, geopositionsBefore);
		writer.WriteInt16((short)geopositionsCount);
		writer.Seek(SeekOrigin.Begin, geopositionsAfter);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		var geopositionsCount = reader.ReadInt16();
		var geopositions = new PartyMemberGeoPosition[geopositionsCount];
		for (var i = 0; i < geopositionsCount; i++)
		{
			var entry = PartyMemberGeoPosition.Empty;
			entry.Deserialize(reader);
			geopositions[i] = entry;
		}
		Geopositions = geopositions;
	}
}
