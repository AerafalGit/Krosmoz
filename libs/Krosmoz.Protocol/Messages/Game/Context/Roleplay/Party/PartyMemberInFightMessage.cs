// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Game.Context;

namespace Krosmoz.Protocol.Messages.Game.Context.Roleplay.Party;

public sealed class PartyMemberInFightMessage : AbstractPartyMessage
{
	public new const uint StaticProtocolId = 6342;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static new PartyMemberInFightMessage Empty =>
		new() { PartyId = 0, Reason = 0, MemberId = 0, MemberAccountId = 0, MemberName = string.Empty, FightId = 0, FightMap = MapCoordinatesExtended.Empty, SecondsBeforeFightStart = 0 };

	public required sbyte Reason { get; set; }

	public required int MemberId { get; set; }

	public required int MemberAccountId { get; set; }

	public required string MemberName { get; set; }

	public required int FightId { get; set; }

	public required MapCoordinatesExtended FightMap { get; set; }

	public required int SecondsBeforeFightStart { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		writer.WriteSByte(Reason);
		writer.WriteInt(MemberId);
		writer.WriteInt(MemberAccountId);
		writer.WriteUtfLengthPrefixed16(MemberName);
		writer.WriteInt(FightId);
		FightMap.Serialize(writer);
		writer.WriteInt(SecondsBeforeFightStart);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		Reason = reader.ReadSByte();
		MemberId = reader.ReadInt();
		MemberAccountId = reader.ReadInt();
		MemberName = reader.ReadUtfLengthPrefixed16();
		FightId = reader.ReadInt();
		FightMap = MapCoordinatesExtended.Empty;
		FightMap.Deserialize(reader);
		SecondsBeforeFightStart = reader.ReadInt();
	}
}
