// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Game.Interactive;
using Krosmoz.Servers.GameServer.Database.Models.Interactives;

namespace Krosmoz.Servers.GameServer.Models.World.Interactives;

/// <summary>
/// Represents a wrapper for a skill, including its unique identifier, ID, and associated action.
/// </summary>
public sealed class SkillWrapper
{
    /// <summary>
    /// Gets or sets the unique identifier of the skill instance.
    /// </summary>
    public int Uid { get; }

    /// <summary>
    /// Gets or sets the ID of the skill.
    /// </summary>
    public uint Id { get; }

    /// <summary>
    /// Gets or sets the associated interactive action record for the skill.
    /// </summary>
    public InteractiveActionRecord? Action { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="SkillWrapper"/> class.
    /// </summary>
    /// <param name="uid">The unique identifier of the skill instance.</param>
    /// <param name="id">The ID of the skill.</param>
    /// <param name="action">The associated interactive action record.</param>
    public SkillWrapper(int uid, uint id, InteractiveActionRecord? action)
    {
        Uid = uid;
        Id = id;
        Action = action;
    }

    /// <summary>
    /// Converts the skill wrapper into an interactive element skill.
    /// If the associated action has a name ID, it creates an <see cref="InteractiveElementNamedSkill"/>;
    /// otherwise, it creates a generic <see cref="InteractiveElementSkill"/>.
    /// </summary>
    /// <returns>An instance of <see cref="InteractiveElementSkill"/> representing the skill.</returns>
    public InteractiveElementSkill GetInteractiveElementSkill()
    {
        if (Action is { NameId: not null })
        {
            return new InteractiveElementNamedSkill
            {
                SkillId = Id,
                SkillInstanceUid = Uid,
                NameId = Action.NameId.Value,
            };
        }

        return new InteractiveElementSkill
        {
            SkillId = Id,
            SkillInstanceUid = Uid
        };
    }
}
