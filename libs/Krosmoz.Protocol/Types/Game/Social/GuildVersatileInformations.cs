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

	public required int GuildId { get; set; }

	public required int LeaderId { get; set; }

	public required ushort GuildLevel { get; set; }

	public required ushort NbMembers { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteInt(GuildId);
		writer.WriteInt(LeaderId);
		writer.WriteUShort(GuildLevel);
		writer.WriteUShort(NbMembers);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		GuildId = reader.ReadInt();
		LeaderId = reader.ReadInt();
		GuildLevel = reader.ReadUShort();
		NbMembers = reader.ReadUShort();
	}
}
