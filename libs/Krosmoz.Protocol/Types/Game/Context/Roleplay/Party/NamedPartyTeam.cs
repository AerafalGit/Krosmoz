// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Types.Game.Context.Roleplay.Party;

public sealed class NamedPartyTeam : DofusType
{
	public new const ushort StaticProtocolId = 469;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static NamedPartyTeam Empty =>
		new() { TeamId = 0, PartyName = string.Empty };

	public required sbyte TeamId { get; set; }

	public required string PartyName { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteInt8(TeamId);
		writer.WriteUtfPrefixedLength16(PartyName);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		TeamId = reader.ReadInt8();
		PartyName = reader.ReadUtfPrefixedLength16();
	}
}
