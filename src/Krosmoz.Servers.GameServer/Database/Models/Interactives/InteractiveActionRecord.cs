// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Servers.GameServer.Database.Models.GameActions;

namespace Krosmoz.Servers.GameServer.Database.Models.Interactives;

public sealed class InteractiveActionRecord : GameActionRecord
{
    public int Id { get; init; }

    public required int InteractiveId { get; init; }

    public required int InteractiveTemplateId { get; init; }

    public required int SkillId { get; init; }

    public uint? NameId { get; init; }
}
