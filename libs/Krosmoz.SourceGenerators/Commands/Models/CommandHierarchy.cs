// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.SourceGenerators.Commands.Models;

public enum CommandHierarchies
{
    Unavailable = -1,
    Player = 0,
    Moderator = 10,
    GamemasterPadawan = 20,
    Gamemaster = 30,
    Admin = 40
}
