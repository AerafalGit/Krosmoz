// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Servers.GameServer.Database.Models.Experiences;

public sealed class ExperienceRecord
{
    public byte Level { get; set; }

    public long CharacterXp { get; set; }

    public long GuildXp { get; set; }

    public long? JobXp { get; set; }

    public long? MountXp { get; set; }

    public long? AlignmentHonor { get; set; }

    public ExperienceRecord(byte level, long characterXp, long guildXp, long? jobXp = null, long? mountXp = null, long? alignmentHonor = null)
    {
        Level = level;
        CharacterXp = characterXp;
        GuildXp = guildXp;
        JobXp = jobXp;
        MountXp = mountXp;
        AlignmentHonor = alignmentHonor;
    }
}
