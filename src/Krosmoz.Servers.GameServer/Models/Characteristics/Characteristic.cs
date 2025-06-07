// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using System.Diagnostics;
using Krosmoz.Protocol.Enums.Custom;
using Krosmoz.Protocol.Types.Game.Character.Characteristic;
using Krosmoz.Servers.GameServer.Database.Models.Characteristics;
using Krosmoz.Servers.GameServer.Models.Actors.Characters;

namespace Krosmoz.Servers.GameServer.Models.Characteristics;

/// <summary>
/// Represents a characteristic of a game character.
/// </summary>
[DebuggerDisplay("{ToString(),nq}")]
public class Characteristic
{
    /// <summary>
    /// Gets the owner of the characteristic.
    /// </summary>
    public CharacterActor Owner { get; }

    /// <summary>
    /// Gets the identifier of the characteristic.
    /// </summary>
    public CharacteristicIds Id { get; }

    /// <summary>
    /// Gets the formulas used to calculate the characteristic's base value.
    /// </summary>
    protected CharacteristicFormulas? Formulas { get; }

    /// <summary>
    /// Gets a value indicating whether the limit applies only to equipped items.
    /// </summary>
    protected bool LimitEquippedOnly { get; }

    /// <summary>
    /// Gets or sets the maximum limit for the characteristic's value.
    /// </summary>
    protected int? ValueLimit { get; set; }

    /// <summary>
    /// Gets or sets the base value of the characteristic.
    /// </summary>
    protected int ValueBase { get; set; }

    /// <summary>
    /// Gets or sets the additional value of the characteristic.
    /// </summary>
    protected int ValueAdditional { get; set; }

    /// <summary>
    /// Gets or sets the context-specific value of the characteristic.
    /// </summary>
    protected int ValueContext { get; set; }

    /// <summary>
    /// Gets or sets the value of the characteristic from equipped items.
    /// </summary>
    protected int ValueEquipped { get; set; }

    /// <summary>
    /// Gets or sets the value of the characteristic given by external sources.
    /// </summary>
    protected int ValueGiven { get; set; }

    /// <summary>
    /// Gets or sets the base value of the characteristic, calculated using formulas if available.
    /// </summary>
    public virtual int Base
    {
        get => Formulas?.Invoke(Owner) + ValueBase ?? ValueBase;
        set => ValueBase = value;
    }

    /// <summary>
    /// Gets or sets the value of the characteristic from equipped items.
    /// </summary>
    public virtual int Equipped
    {
        get => ValueEquipped;
        set => ValueEquipped = value;
    }

    /// <summary>
    /// Gets or sets the value of the characteristic given by external sources.
    /// </summary>
    public virtual int Given
    {
        get => ValueGiven;
        set => ValueGiven = value;
    }

    /// <summary>
    /// Gets or sets the context-specific value of the characteristic.
    /// </summary>
    public virtual int Context
    {
        get => ValueContext;
        set => ValueContext = value;
    }

    /// <summary>
    /// Gets or sets the additional value of the characteristic.
    /// </summary>
    public virtual int Additional
    {
        get => ValueAdditional;
        set => ValueAdditional = value;
    }

    /// <summary>
    /// Gets the total value of the characteristic, including all modifiers and limits.
    /// </summary>
    public virtual int Total
    {
        get
        {
            var totalNoBoost = Base + Additional + Equipped;

            if (LimitEquippedOnly && totalNoBoost > Limit)
                totalNoBoost = Limit.Value;

            var total = totalNoBoost + Given + Context;

            if (Limit is not null && !LimitEquippedOnly && total > Limit.Value)
                total = Limit.Value;

            return total;
        }
    }

    /// <summary>
    /// Gets the total value of the characteristic, ensuring it is non-negative.
    /// </summary>
    public virtual int TotalSafe =>
        Total > 0 ? Total : 0;

    /// <summary>
    /// Gets the total value of the characteristic excluding the context-specific value.
    /// </summary>
    public virtual int TotalWithoutContext =>
        Total - Context;

    /// <summary>
    /// Gets or sets the limit for the characteristic's value.
    /// </summary>
    protected virtual int? Limit
    {
        get => ValueLimit;
        set => ValueLimit = value;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Characteristic"/> class with formulas.
    /// </summary>
    /// <param name="owner">The owner of the characteristic.</param>
    /// <param name="id">The identifier of the characteristic.</param>
    /// <param name="valueBase">The base value of the characteristic.</param>
    /// <param name="formulas">The formulas used to calculate the base value.</param>
    public Characteristic(CharacterActor owner, CharacteristicIds id, int valueBase, CharacteristicFormulas? formulas)
    {
        Owner = owner;
        Id = id;
        ValueBase = valueBase;
        Formulas = formulas;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Characteristic"/> class with a value limit.
    /// </summary>
    /// <param name="owner">The owner of the characteristic.</param>
    /// <param name="id">The identifier of the characteristic.</param>
    /// <param name="valueBase">The base value of the characteristic.</param>
    /// <param name="valueLimit">The maximum limit for the characteristic's value.</param>
    /// <param name="limitEquippedOnly">Indicates whether the limit applies only to equipped items.</param>
    public Characteristic(CharacterActor owner, CharacteristicIds id, int valueBase, int? valueLimit = null, bool limitEquippedOnly = false)
    {
        Owner = owner;
        Id = id;
        ValueBase = valueBase;
        ValueLimit = valueLimit;
        LimitEquippedOnly = limitEquippedOnly;
    }

    /// <summary>
    /// Creates a copy of the characteristic with the same owner.
    /// </summary>
    /// <returns>A new <see cref="Characteristic"/> instance.</returns>
    public virtual Characteristic Clone()
    {
        return CloneAndChangeOwner(Owner);
    }

    /// <summary>
    /// Creates a copy of the characteristic with a new owner.
    /// </summary>
    /// <param name="owner">The new owner of the characteristic.</param>
    /// <returns>A new <see cref="Characteristic"/> instance.</returns>
    public virtual Characteristic CloneAndChangeOwner(CharacterActor owner)
    {
        return new Characteristic(owner, Id, Base, Limit, LimitEquippedOnly)
        {
            Additional = Additional,
            Context = Context,
            Equipped = Equipped,
            Given = Given
        };
    }

    /// <summary>
    /// Resets the characteristic's context-specific value and creates a copy with a new owner.
    /// </summary>
    /// <param name="owner">The new owner of the characteristic.</param>
    /// <returns>A new <see cref="Characteristic"/> instance.</returns>
    public virtual Characteristic Reset(CharacterActor owner)
    {
        return new Characteristic(owner, Id, Base, Limit, LimitEquippedOnly)
        {
            Additional = Additional,
            Context = 0,
            Equipped = Equipped,
            Given = Given
        };
    }

    /// <summary>
    /// Converts the characteristic to a <see cref="CharacterBaseCharacteristic"/> instance.
    /// </summary>
    /// <returns>A <see cref="CharacterBaseCharacteristic"/> instance representing the characteristic.</returns>
    public virtual CharacterBaseCharacteristic GetCharacterBaseCharacteristic()
    {
        return new CharacterBaseCharacteristic
        {
            Base = (short)Base,
            Additionnal = (short)Additional,
            ObjectsAndMountBonus = (short)Equipped,
            AlignGiftBonus = (short)Given,
            ContextModif = (short)Context
        };
    }

    /// <summary>
    /// Retrieves the characteristic data, including the base value and additional bonus.
    /// </summary>
    /// <returns>
    /// A <see cref="CharacteristicData"/> object containing the base value and additional bonus of the characteristic.
    /// </returns>
    public virtual CharacteristicData GetCharacteristicData()
    {
        return new CharacteristicData
        {
            Base = Base,
            Bonus = Additional
        };
    }

    /// <summary>
    /// Returns a string representation of the characteristic.
    /// </summary>
    /// <returns>
    /// A string containing the characteristic's ID, total value, and breakdown of its components
    /// (base, additional, equipped, and context-specific values).
    /// </returns>
    public override string ToString()
    {
        return $"{Id} [{Total}] ({Base}+{Additional}+{Equipped}+{Context})";
    }

    /// <summary>
    /// Adds the total value of a <see cref="Characteristic"/> to an integer.
    /// </summary>
    /// <param name="left">The integer value.</param>
    /// <param name="right">The <see cref="Characteristic"/> instance.</param>
    /// <returns>The sum of the integer and the characteristic's total value.</returns>
    public static int operator +(int left, Characteristic right)
    {
        return left + right.Total;
    }

    /// <summary>
    /// Adds the total values of two <see cref="Characteristic"/> instances.
    /// </summary>
    /// <param name="left">The first <see cref="Characteristic"/> instance.</param>
    /// <param name="right">The second <see cref="Characteristic"/> instance.</param>
    /// <returns>The sum of the total values of both characteristics.</returns>
    public static int operator +(Characteristic left, Characteristic right)
    {
        return left.Total + right.Total;
    }

    /// <summary>
    /// Subtracts the total value of a <see cref="Characteristic"/> from an integer.
    /// </summary>
    /// <param name="left">The integer value.</param>
    /// <param name="right">The <see cref="Characteristic"/> instance.</param>
    /// <returns>The result of subtracting the characteristic's total value from the integer.</returns>
    public static int operator -(int left, Characteristic right)
    {
        return left - right.Total;
    }

    /// <summary>
    /// Subtracts the total value of one <see cref="Characteristic"/> from another.
    /// </summary>
    /// <param name="left">The first <see cref="Characteristic"/> instance.</param>
    /// <param name="right">The second <see cref="Characteristic"/> instance.</param>
    /// <returns>The result of subtracting the second characteristic's total value from the first.</returns>
    public static int operator -(Characteristic left, Characteristic right)
    {
        return left.Total - right.Total;
    }

    /// <summary>
    /// Multiplies an integer by the total value of a <see cref="Characteristic"/>.
    /// </summary>
    /// <param name="left">The integer value.</param>
    /// <param name="right">The <see cref="Characteristic"/> instance.</param>
    /// <returns>The product of the integer and the characteristic's total value.</returns>
    public static int operator *(int left, Characteristic right)
    {
        return left * right.Total;
    }

    /// <summary>
    /// Multiplies the total values of two <see cref="Characteristic"/> instances.
    /// </summary>
    /// <param name="left">The first <see cref="Characteristic"/> instance.</param>
    /// <param name="right">The second <see cref="Characteristic"/> instance.</param>
    /// <returns>The product of the total values of both characteristics.</returns>
    public static int operator *(Characteristic left, Characteristic right)
    {
        return left.Total * right.Total;
    }

    /// <summary>
    /// Divides the total value of a <see cref="Characteristic"/> by a double.
    /// </summary>
    /// <param name="left">The <see cref="Characteristic"/> instance.</param>
    /// <param name="right">The divisor as a double.</param>
    /// <returns>The result of dividing the characteristic's total value by the double.</returns>
    public static double operator /(Characteristic left, double right)
    {
        return left.Total / right;
    }

    /// <summary>
    /// Divides the total value of one <see cref="Characteristic"/> by another.
    /// </summary>
    /// <param name="left">The first <see cref="Characteristic"/> instance.</param>
    /// <param name="right">The second <see cref="Characteristic"/> instance.</param>
    /// <returns>The result of dividing the first characteristic's total value by the second's total value.</returns>
    public static double operator /(Characteristic left, Characteristic right)
    {
        return (double)left.Total / right.Total;
    }
}
