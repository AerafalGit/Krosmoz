// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Game.Social;

namespace Krosmoz.Protocol.Messages.Game.Alliance;

public sealed class AllianceVersatileInfoListMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6436;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static AllianceVersatileInfoListMessage Empty =>
		new() { Alliances = [] };

	public required IEnumerable<AllianceVersatileInformations> Alliances { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		var alliancesBefore = writer.Position;
		var alliancesCount = 0;
		writer.WriteInt16(0);
		foreach (var item in Alliances)
		{
			item.Serialize(writer);
			alliancesCount++;
		}
		var alliancesAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, alliancesBefore);
		writer.WriteInt16((short)alliancesCount);
		writer.Seek(SeekOrigin.Begin, alliancesAfter);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		var alliancesCount = reader.ReadInt16();
		var alliances = new AllianceVersatileInformations[alliancesCount];
		for (var i = 0; i < alliancesCount; i++)
		{
			var entry = AllianceVersatileInformations.Empty;
			entry.Deserialize(reader);
			alliances[i] = entry;
		}
		Alliances = alliances;
	}
}
