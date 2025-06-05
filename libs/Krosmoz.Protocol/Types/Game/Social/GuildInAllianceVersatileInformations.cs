// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Types.Game.Social;

public sealed class GuildInAllianceVersatileInformations : GuildVersatileInformations
{
	public new const ushort StaticProtocolId = 437;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static new GuildInAllianceVersatileInformations Empty =>
		new() { NbMembers = 0, GuildLevel = 0, LeaderId = 0, GuildId = 0, AllianceId = 0 };

	public required uint AllianceId { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		writer.WriteVarUInt32(AllianceId);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		AllianceId = reader.ReadVarUInt32();
	}
}
