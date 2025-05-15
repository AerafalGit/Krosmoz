// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Game.Guild;

namespace Krosmoz.Protocol.Types.Game.Context.Roleplay;

public sealed class GuildInAllianceInformations : GuildInformations
{
	public new const ushort StaticProtocolId = 420;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static new GuildInAllianceInformations Empty =>
		new() { GuildName = string.Empty, GuildId = 0, GuildEmblem = GuildEmblem.Empty, GuildLevel = 0, NbMembers = 0, Enabled = false };

	public required ushort GuildLevel { get; set; }

	public required ushort NbMembers { get; set; }

	public required bool Enabled { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		writer.WriteUInt16(GuildLevel);
		writer.WriteUInt16(NbMembers);
		writer.WriteBoolean(Enabled);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		GuildLevel = reader.ReadUInt16();
		NbMembers = reader.ReadUInt16();
		Enabled = reader.ReadBoolean();
	}
}
