// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Servers.GameServer.Database.Models.Experiences;

public sealed class ExperienceRecord
{
    public byte Level { get; set; }

    public ulong CharacterXp { get; set; }

    public ulong GuildXp { get; set; }

    public ulong? JobXp { get; set; }

    public ulong? MountXp { get; set; }

    public ulong? AlignmentHonor { get; set; }

    public ExperienceRecord(byte level, ulong characterXp, ulong guildXp, ulong? jobXp = null, ulong? mountXp = null, ulong? alignmentHonor = null)
    {
        Level = level;
        CharacterXp = characterXp;
        GuildXp = guildXp;
        JobXp = jobXp;
        MountXp = mountXp;
        AlignmentHonor = alignmentHonor;
    }
}
