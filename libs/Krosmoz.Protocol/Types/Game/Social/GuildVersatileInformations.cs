// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Types.Game.Social;

public class GuildVersatileInformations : DofusType
{
	public new const ushort StaticProtocolId = 435;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static GuildVersatileInformations Empty =>
		new() { GuildId = 0, LeaderId = 0, GuildLevel = 0, NbMembers = 0 };

	public required uint GuildId { get; set; }

	public required uint LeaderId { get; set; }

	public required byte GuildLevel { get; set; }

	public required byte NbMembers { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteVarUInt32(GuildId);
		writer.WriteVarUInt32(LeaderId);
		writer.WriteUInt8(GuildLevel);
		writer.WriteUInt8(NbMembers);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		GuildId = reader.ReadVarUInt32();
		LeaderId = reader.ReadVarUInt32();
		GuildLevel = reader.ReadUInt8();
		NbMembers = reader.ReadUInt8();
	}
}
