// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Game.Context.Roleplay;

namespace Krosmoz.Protocol.Messages.Game.Alliance;

public sealed class KohUpdateMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6439;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static KohUpdateMessage Empty =>
		new() { Alliances = [], AllianceNbMembers = [], AllianceRoundWeigth = [], AllianceMatchScore = [], AllianceMapWinner = BasicAllianceInformations.Empty, AllianceMapWinnerScore = 0, AllianceMapMyAllianceScore = 0 };

	public required IEnumerable<AllianceInformations> Alliances { get; set; }

	public required IEnumerable<short> AllianceNbMembers { get; set; }

	public required IEnumerable<int> AllianceRoundWeigth { get; set; }

	public required IEnumerable<sbyte> AllianceMatchScore { get; set; }

	public required BasicAllianceInformations AllianceMapWinner { get; set; }

	public required int AllianceMapWinnerScore { get; set; }

	public required int AllianceMapMyAllianceScore { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		var alliancesBefore = writer.Position;
		var alliancesCount = 0;
		writer.WriteShort(0);
		foreach (var item in Alliances)
		{
			item.Serialize(writer);
			alliancesCount++;
		}
		var alliancesAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, alliancesBefore);
		writer.WriteShort((short)alliancesCount);
		writer.Seek(SeekOrigin.Begin, alliancesAfter);
		var allianceNbMembersBefore = writer.Position;
		var allianceNbMembersCount = 0;
		writer.WriteShort(0);
		foreach (var item in AllianceNbMembers)
		{
			writer.WriteShort(item);
			allianceNbMembersCount++;
		}
		var allianceNbMembersAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, allianceNbMembersBefore);
		writer.WriteShort((short)allianceNbMembersCount);
		writer.Seek(SeekOrigin.Begin, allianceNbMembersAfter);
		var allianceRoundWeigthBefore = writer.Position;
		var allianceRoundWeigthCount = 0;
		writer.WriteShort(0);
		foreach (var item in AllianceRoundWeigth)
		{
			writer.WriteInt(item);
			allianceRoundWeigthCount++;
		}
		var allianceRoundWeigthAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, allianceRoundWeigthBefore);
		writer.WriteShort((short)allianceRoundWeigthCount);
		writer.Seek(SeekOrigin.Begin, allianceRoundWeigthAfter);
		var allianceMatchScoreBefore = writer.Position;
		var allianceMatchScoreCount = 0;
		writer.WriteShort(0);
		foreach (var item in AllianceMatchScore)
		{
			writer.WriteSByte(item);
			allianceMatchScoreCount++;
		}
		var allianceMatchScoreAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, allianceMatchScoreBefore);
		writer.WriteShort((short)allianceMatchScoreCount);
		writer.Seek(SeekOrigin.Begin, allianceMatchScoreAfter);
		AllianceMapWinner.Serialize(writer);
		writer.WriteInt(AllianceMapWinnerScore);
		writer.WriteInt(AllianceMapMyAllianceScore);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		var alliancesCount = reader.ReadShort();
		var alliances = new AllianceInformations[alliancesCount];
		for (var i = 0; i < alliancesCount; i++)
		{
			var entry = AllianceInformations.Empty;
			entry.Deserialize(reader);
			alliances[i] = entry;
		}
		Alliances = alliances;
		var allianceNbMembersCount = reader.ReadShort();
		var allianceNbMembers = new short[allianceNbMembersCount];
		for (var i = 0; i < allianceNbMembersCount; i++)
		{
			allianceNbMembers[i] = reader.ReadShort();
		}
		AllianceNbMembers = allianceNbMembers;
		var allianceRoundWeigthCount = reader.ReadShort();
		var allianceRoundWeigth = new int[allianceRoundWeigthCount];
		for (var i = 0; i < allianceRoundWeigthCount; i++)
		{
			allianceRoundWeigth[i] = reader.ReadInt();
		}
		AllianceRoundWeigth = allianceRoundWeigth;
		var allianceMatchScoreCount = reader.ReadShort();
		var allianceMatchScore = new sbyte[allianceMatchScoreCount];
		for (var i = 0; i < allianceMatchScoreCount; i++)
		{
			allianceMatchScore[i] = reader.ReadSByte();
		}
		AllianceMatchScore = allianceMatchScore;
		AllianceMapWinner = BasicAllianceInformations.Empty;
		AllianceMapWinner.Deserialize(reader);
		AllianceMapWinnerScore = reader.ReadInt();
		AllianceMapMyAllianceScore = reader.ReadInt();
	}
}
