// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Game.Context.Roleplay;
using Krosmoz.Protocol.Types.Game.Guild;

namespace Krosmoz.Protocol.Types.Game.Social;

public class GuildFactSheetInformations : GuildInformations
{
	public new const ushort StaticProtocolId = 424;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static new GuildFactSheetInformations Empty =>
		new() { GuildName = string.Empty, GuildId = 0, GuildEmblem = GuildEmblem.Empty, LeaderId = 0, GuildLevel = 0, NbMembers = 0 };

	public required int LeaderId { get; set; }

	public required byte GuildLevel { get; set; }

	public required short NbMembers { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		writer.WriteInt(LeaderId);
		writer.WriteByte(GuildLevel);
		writer.WriteShort(NbMembers);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		LeaderId = reader.ReadInt();
		GuildLevel = reader.ReadByte();
		NbMembers = reader.ReadShort();
	}
}
