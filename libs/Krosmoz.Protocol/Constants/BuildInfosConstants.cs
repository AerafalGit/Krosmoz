// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Enums;
using Version = Krosmoz.Protocol.Types.Version.Version;

namespace Krosmoz.Protocol.Constants;

public static class BuildInfosConstants
{
    public static readonly Version BuildVersion = new()
    {
        Major = 2,
        Minor = 29,
        BuildType = (sbyte)BuildType,
        Revision = BuildRevision,
        Patch = BuildPatch,
        Release = 0
    };

    public const BuildTypes BuildType = BuildTypes.Beta;

    public const int BuildRevision = 95336;

    public const int BuildPatch = 0;

    public const string BuildDate = "Jun 4, 2015 - 09:22:13 CEST";
}
