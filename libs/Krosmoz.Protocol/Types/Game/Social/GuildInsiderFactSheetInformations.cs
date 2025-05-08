// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Game.Guild;

namespace Krosmoz.Protocol.Types.Game.Social;

public sealed class GuildInsiderFactSheetInformations : GuildFactSheetInformations
{
	public new const ushort StaticProtocolId = 423;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static new GuildInsiderFactSheetInformations Empty =>
		new() { GuildName = string.Empty, GuildId = 0, GuildEmblem = GuildEmblem.Empty, NbMembers = 0, GuildLevel = 0, LeaderId = 0, LeaderName = string.Empty, NbConnectedMembers = 0, NbTaxCollectors = 0, LastActivity = 0, Enabled = false };

	public required string LeaderName { get; set; }

	public required short NbConnectedMembers { get; set; }

	public required sbyte NbTaxCollectors { get; set; }

	public required int LastActivity { get; set; }

	public required bool Enabled { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		writer.WriteUtfLengthPrefixed16(LeaderName);
		writer.WriteShort(NbConnectedMembers);
		writer.WriteSByte(NbTaxCollectors);
		writer.WriteInt(LastActivity);
		writer.WriteBoolean(Enabled);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		LeaderName = reader.ReadUtfLengthPrefixed16();
		NbConnectedMembers = reader.ReadShort();
		NbTaxCollectors = reader.ReadSByte();
		LastActivity = reader.ReadInt();
		Enabled = reader.ReadBoolean();
	}
}
