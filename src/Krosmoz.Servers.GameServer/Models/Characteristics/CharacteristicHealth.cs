// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Enums.Custom;
using Krosmoz.Servers.GameServer.Models.Actors.Characters;

namespace Krosmoz.Servers.GameServer.Models.Characteristics;

/// <summary>
/// Represents the health characteristic of a game character.
/// </summary>
public sealed class CharacteristicHealth : Characteristic
{
    /// <summary>
    /// Stores the safe value of damage taken, ensuring it does not exceed the maximum total health.
    /// </summary>
    private int _damageTakenSafe;

    /// <summary>
    /// Stores the raw value of damage taken.
    /// </summary>
    private int _damageTaken;

    /// <summary>
    /// Stores the permanent damage value, which reduces the maximum health.
    /// </summary>
    private int _permanentDamages;

    /// <summary>
    /// Gets or sets the base value of the health characteristic.
    /// Adjusts damage taken when the base value changes.
    /// </summary>
    public override int Base
    {
        get => ValueBase;
        set
        {
            ValueBase = value;
            AdjustDamageTaken();
        }
    }

    /// <summary>
    /// Gets or sets the value of the health characteristic from equipped items.
    /// Adjusts damage taken when the equipped value changes.
    /// </summary>
    public override int Equipped
    {
        get => ValueEquipped;
        set
        {
            ValueEquipped = value;
            AdjustDamageTaken();
        }
    }

    /// <summary>
    /// Gets or sets the value of the health characteristic given by external sources.
    /// Adjusts damage taken when the given value changes.
    /// </summary>
    public override int Given
    {
        get => ValueGiven;
        set
        {
            ValueGiven = value;
            AdjustDamageTaken();
        }
    }

    /// <summary>
    /// Gets or sets the context-specific value of the health characteristic.
    /// Adjusts damage taken when the context value changes.
    /// </summary>
    public override int Context
    {
        get => ValueContext;
        set
        {
            ValueContext = value;
            AdjustDamageTaken();
        }
    }

    /// <summary>
    /// Gets or sets the amount of damage taken by the character.
    /// Ensures the damage taken does not exceed the maximum total health.
    /// </summary>
    public int DamageTaken
    {
        get => _damageTakenSafe;
        set
        {
            _damageTaken = value;
            _damageTakenSafe = value > TotalMax ? TotalMax : value;
        }
    }

    /// <summary>
    /// Gets or sets the permanent damage value, which reduces the maximum health.
    /// Ensures the permanent damage does not result in negative health.
    /// </summary>
    public int PermanentDamages
    {
        get => _permanentDamages;
        set
        {
            if (TotalSafe - value < 0)
                _permanentDamages = Base + Equipped + Given + Context + Owner.Characteristics.Vitality.Total - 1;
            else
                _permanentDamages = value;

            if (TotalSafe > TotalMax)
                DamageTaken += TotalSafe - TotalMax;
        }
    }

    /// <summary>
    /// Gets the total health value, ensuring it is non-negative.
    /// </summary>
    public override int Total =>
        TotalSafe;

    /// <summary>
    /// Gets the safe total health value after subtracting damage taken.
    /// Ensures the value is non-negative.
    /// </summary>
    public override int TotalSafe
    {
        get
        {
            var totalSafe = TotalMax - DamageTaken;

            return totalSafe < 0 ? 0 : totalSafe;
        }
    }

    /// <summary>
    /// Gets the maximum total health value, considering all modifiers and permanent damage.
    /// Ensures the value is non-negative.
    /// </summary>
    public int TotalMax
    {
        get
        {
            var totalMax = Base + Equipped + Given + Context + Owner.Characteristics.Vitality.Total - PermanentDamages;

            return totalMax < 0 ? 0 : totalMax;
        }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="CharacteristicHealth"/> class with formulas.
    /// </summary>
    /// <param name="owner">The owner of the health characteristic.</param>
    /// <param name="id">The identifier of the health characteristic.</param>
    /// <param name="valueBase">The base value of the health characteristic.</param>
    /// <param name="formulas">The formulas used to calculate the base value.</param>
    public CharacteristicHealth(CharacterActor owner, CharacteristicIds id, int valueBase, CharacteristicFormulas? formulas)
        : base(owner, id, valueBase, formulas)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="CharacteristicHealth"/> class with a value limit.
    /// </summary>
    /// <param name="owner">The owner of the health characteristic.</param>
    /// <param name="id">The identifier of the health characteristic.</param>
    /// <param name="valueBase">The base value of the health characteristic.</param>
    /// <param name="valueLimit">The maximum limit for the health characteristic's value.</param>
    /// <param name="limitEquippedOnly">Indicates whether the limit applies only to equipped items.</param>
    public CharacteristicHealth(CharacterActor owner, CharacteristicIds id, int valueBase, int? valueLimit = null, bool limitEquippedOnly = false)
        : base(owner, id, valueBase, valueLimit, limitEquippedOnly)
    {
    }

    /// <summary>
    /// Creates a copy of the health characteristic with a new owner.
    /// </summary>
    /// <param name="owner">The new owner of the health characteristic.</param>
    /// <returns>A new <see cref="CharacteristicHealth"/> instance.</returns>
    public override Characteristic CloneAndChangeOwner(CharacterActor owner)
    {
        return new CharacteristicHealth(owner, Id, Base, Limit, LimitEquippedOnly)
        {
            Additional = Additional,
            Context = Context,
            Equipped = Equipped,
            Given = Given
        };
    }

    /// <summary>
    /// Resets the health characteristic's context-specific value and creates a copy with a new owner.
    /// </summary>
    /// <param name="owner">The new owner of the health characteristic.</param>
    /// <returns>A new <see cref="CharacteristicHealth"/> instance.</returns>
    public override Characteristic Reset(CharacterActor owner)
    {
        return new CharacteristicHealth(owner, Id, Base, Limit, LimitEquippedOnly)
        {
            Additional = Additional,
            Context = 0,
            Equipped = Equipped,
            Given = Given
        };
    }

    /// <summary>
    /// Adjusts the damage taken values to ensure they remain within valid bounds.
    /// </summary>
    private void AdjustDamageTaken()
    {
        if (_damageTakenSafe > TotalMax)
        {
            _damageTaken = _damageTakenSafe;
            _damageTakenSafe = TotalMax;
        }
        else
        {
            if (_damageTaken > _damageTakenSafe)
                _damageTakenSafe = _damageTaken;
        }
    }
}
