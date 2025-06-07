// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Enums.Custom;
using Krosmoz.Servers.GameServer.Models.Actors.Characters;

namespace Krosmoz.Servers.GameServer.Models.Characteristics;

/// <summary>
/// Represents the initiative characteristic of a game character.
/// Initiative is calculated based on the character's health and other characteristics.
/// </summary>
public sealed class CharacteristicInitiative : Characteristic
{
    /// <summary>
    /// Gets the base value of the initiative characteristic.
    /// The value is calculated based on the character's health and the sum of chance, intelligence, agility, and strength.
    /// </summary>
    public override int Base
    {
        get
        {
            var health = Owner.Characteristics.Health;
            var chance = Owner.Characteristics.Chance;
            var intelligence = Owner.Characteristics.Intelligence;
            var agility = Owner.Characteristics.Agility;
            var strength = Owner.Characteristics.Strength;

            return health.Total > 0
                ? (int)((chance + intelligence + agility + strength) * ((double)health.Total / health.TotalMax))
                : 0;
        }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="CharacteristicInitiative"/> class with formulas.
    /// </summary>
    /// <param name="owner">The owner of the initiative characteristic.</param>
    /// <param name="id">The identifier of the initiative characteristic.</param>
    /// <param name="valueBase">The base value of the initiative characteristic.</param>
    /// <param name="formulas">The formulas used to calculate the base value.</param>
    public CharacteristicInitiative(CharacterActor owner, CharacteristicIds id, int valueBase, CharacteristicFormulas? formulas)
        : base(owner, id, valueBase, formulas)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="CharacteristicInitiative"/> class with a value limit.
    /// </summary>
    /// <param name="owner">The owner of the initiative characteristic.</param>
    /// <param name="id">The identifier of the initiative characteristic.</param>
    /// <param name="valueBase">The base value of the initiative characteristic.</param>
    /// <param name="valueLimit">The maximum limit for the initiative characteristic's value.</param>
    /// <param name="limitEquippedOnly">Indicates whether the limit applies only to equipped items.</param>
    public CharacteristicInitiative(CharacterActor owner, CharacteristicIds id, int valueBase, int? valueLimit = null, bool limitEquippedOnly = false)
        : base(owner, id, valueBase, valueLimit, limitEquippedOnly)
    {
    }

    /// <summary>
    /// Creates a copy of the initiative characteristic with a new owner.
    /// </summary>
    /// <param name="owner">The new owner of the initiative characteristic.</param>
    /// <returns>A new <see cref="CharacteristicInitiative"/> instance.</returns>
    public override Characteristic CloneAndChangeOwner(CharacterActor owner)
    {
        return new CharacteristicInitiative(owner, Id, Base, Limit, LimitEquippedOnly)
        {
            Additional = Additional,
            Context = Context,
            Equipped = Equipped,
            Given = Given
        };
    }

    /// <summary>
    /// Resets the initiative characteristic's context-specific value and creates a copy with a new owner.
    /// </summary>
    /// <param name="owner">The new owner of the initiative characteristic.</param>
    /// <returns>A new <see cref="CharacteristicInitiative"/> instance.</returns>
    public override Characteristic Reset(CharacterActor owner)
    {
        return new CharacteristicInitiative(owner, Id, Base, Limit, LimitEquippedOnly)
        {
            Additional = Additional,
            Context = 0,
            Equipped = Equipped,
            Given = Given
        };
    }
}
