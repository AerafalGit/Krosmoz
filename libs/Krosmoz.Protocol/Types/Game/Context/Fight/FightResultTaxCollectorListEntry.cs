// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Game.Context.Roleplay;

namespace Krosmoz.Protocol.Types.Game.Context.Fight;

public sealed class FightResultTaxCollectorListEntry : FightResultFighterListEntry
{
	public new const ushort StaticProtocolId = 84;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static new FightResultTaxCollectorListEntry Empty =>
		new() { Rewards = FightLoot.Empty, Wave = 0, Outcome = 0, Alive = false, Id = 0, Level = 0, GuildInfo = BasicGuildInformations.Empty, ExperienceForGuild = 0 };

	public required byte Level { get; set; }

	public required BasicGuildInformations GuildInfo { get; set; }

	public required int ExperienceForGuild { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		writer.WriteUInt8(Level);
		GuildInfo.Serialize(writer);
		writer.WriteInt32(ExperienceForGuild);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		Level = reader.ReadUInt8();
		GuildInfo = BasicGuildInformations.Empty;
		GuildInfo.Deserialize(reader);
		ExperienceForGuild = reader.ReadInt32();
	}
}
