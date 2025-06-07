// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Game.Context.Roleplay;

namespace Krosmoz.Servers.GameServer.Models.Actors;

/// <summary>
/// Represents a named actor in the game world.
/// A named actor is an extension of the base <see cref="Actor"/> class
/// with additional properties for retrieving its name and name prefix.
/// </summary>
public abstract class NamedActor : Actor
{
    /// <summary>
    /// Gets the prefix for the actor's name.
    /// </summary>
    public abstract string NamePrefix { get; }

    /// <summary>
    /// Gets the full name of the actor, which is a combination of the name prefix and its unique identifier.
    /// </summary>
    public abstract string Name { get; set; }

    /// <summary>
    /// Retrieves the game roleplay actor information specific to a named actor,
    /// including its ID, disposition, appearance, and name.
    /// </summary>
    /// <returns>
    /// An instance of <see cref="GameRolePlayNamedActorInformations"/> containing the named actor's roleplay data.
    /// </returns>
    public override GameRolePlayActorInformations GetGameRolePlayActorInformations()
    {
        return new GameRolePlayNamedActorInformations
        {
            ContextualId = Id,
            Disposition = GetEntityDispositionInformations(),
            Look = Look.GetEntityLook(),
            Name = Name
        };
    }
}
