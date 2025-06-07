// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Enums.Custom;
using Krosmoz.Servers.GameServer.Models.Actors.Characters;

namespace Krosmoz.Servers.GameServer.Models.Characteristics;

/// <summary>
/// Represents a usable characteristic of a game character.
/// </summary>
public sealed class CharacteristicUsable : Characteristic
{
    /// <summary>
    /// Gets or sets the amount of the characteristic that has been used.
    /// </summary>
    public short Used { get; set; }

    /// <summary>
    /// Gets or sets the amount of the characteristic used by the caster.
    /// </summary>
    public int UsedByCaster { get; set; }

    /// <summary>
    /// Gets or sets the amount of the characteristic available at the start of the round.
    /// </summary>
    public int AvailableAtRoundStart { get; set; }

    /// <summary>
    /// Gets the maximum total value of the characteristic.
    /// </summary>
    public int TotalMax =>
        base.Total;

    /// <summary>
    /// Gets the total value of the characteristic after subtracting the used amount.
    /// </summary>
    public override int Total =>
        TotalMax - Used;

    /// <summary>
    /// Initializes a new instance of the <see cref="CharacteristicUsable"/> class with formulas.
    /// </summary>
    /// <param name="owner">The owner of the characteristic.</param>
    /// <param name="id">The identifier of the characteristic.</param>
    /// <param name="valueBase">The base value of the characteristic.</param>
    /// <param name="formulas">The formulas used to calculate the base value.</param>
    public CharacteristicUsable(CharacterActor owner, CharacteristicIds id, int valueBase, CharacteristicFormulas? formulas)
        : base(owner, id, valueBase, formulas)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="CharacteristicUsable"/> class with a value limit.
    /// </summary>
    /// <param name="owner">The owner of the characteristic.</param>
    /// <param name="id">The identifier of the characteristic.</param>
    /// <param name="valueBase">The base value of the characteristic.</param>
    /// <param name="valueLimit">The maximum limit for the characteristic's value.</param>
    /// <param name="limitEquippedOnly">Indicates whether the limit applies only to equipped items.</param>
    public CharacteristicUsable(CharacterActor owner, CharacteristicIds id, int valueBase, int? valueLimit = null, bool limitEquippedOnly = false)
        : base(owner, id, valueBase, valueLimit, limitEquippedOnly)
    {
    }

    /// <summary>
    /// Creates a copy of the usable characteristic with a new owner.
    /// </summary>
    /// <param name="owner">The new owner of the characteristic.</param>
    /// <returns>A new <see cref="CharacteristicUsable"/> instance.</returns>
    public override Characteristic CloneAndChangeOwner(CharacterActor owner)
    {
        return new CharacteristicUsable(owner, Id, Base, Limit, LimitEquippedOnly)
        {
            Additional = Additional,
            Context = Context,
            Equipped = Equipped,
            Given = Given
        };
    }

    /// <summary>
    /// Resets the usable characteristic's context-specific value and creates a copy with a new owner.
    /// </summary>
    /// <param name="owner">The new owner of the characteristic.</param>
    /// <returns>A new <see cref="CharacteristicUsable"/> instance.</returns>
    public override Characteristic Reset(CharacterActor owner)
    {
        return new CharacteristicUsable(owner, Id, Base, Limit, LimitEquippedOnly)
        {
            Additional = Additional,
            Context = 0,
            Equipped = Equipped,
            Given = Given
        };
    }
}
