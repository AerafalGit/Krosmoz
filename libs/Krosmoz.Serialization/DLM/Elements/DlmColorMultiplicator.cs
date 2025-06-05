// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Serialization.DLM.Elements;

/// <summary>
/// Represents a color multiplicator used for modifying color values.
/// </summary>
public readonly struct ColorMultiplicator
{
    /// <summary>
    /// Indicates whether the color multiplicator is equivalent to one (no modification).
    /// </summary>
    public readonly bool IsOne;

    /// <summary>
    /// The blue component of the color multiplicator.
    /// </summary>
    public readonly int Blue;

    /// <summary>
    /// The green component of the color multiplicator.
    /// </summary>
    public readonly int Green;

    /// <summary>
    /// The red component of the color multiplicator.
    /// </summary>
    public readonly int Red;

    /// <summary>
    /// Initializes a new instance of the <see cref="ColorMultiplicator"/> struct.
    /// </summary>
    /// <param name="red">The red component of the color multiplicator.</param>
    /// <param name="green">The green component of the color multiplicator.</param>
    /// <param name="blue">The blue component of the color multiplicator.</param>
    /// <param name="force">Indicates whether to force the initialization without checking if the sum of components is zero.</param>
    public ColorMultiplicator(int red, int green, int blue, bool force = false)
    {
        Red = red;
        Green = green;
        Blue = blue;

        if (!force && Red + Green + Blue is 0)
            IsOne = true;
    }

    /// <summary>
    /// Multiplies the current color multiplicator with another color multiplicator.
    /// </summary>
    /// <param name="colorMultiplicator">The color multiplicator to multiply with.</param>
    /// <returns>A new <see cref="ColorMultiplicator"/> representing the result of the multiplication.</returns>
    public ColorMultiplicator Multiply(ColorMultiplicator colorMultiplicator)
    {
        if (IsOne)
            return colorMultiplicator;

        if (colorMultiplicator.IsOne)
            return this;

        var red = Clamp(Red + colorMultiplicator.Red, sbyte.MinValue, sbyte.MaxValue);
        var green = Clamp(Green + colorMultiplicator.Green, sbyte.MinValue, -sbyte.MaxValue);
        var blue = Clamp(Blue + colorMultiplicator.Blue, sbyte.MinValue, sbyte.MaxValue);

        return new ColorMultiplicator(red, green, blue, true);
    }

    /// <summary>
    /// Clamps a value to ensure it falls within the specified range.
    /// </summary>
    /// <param name="value">The value to clamp.</param>
    /// <param name="min">The minimum allowable value.</param>
    /// <param name="max">The maximum allowable value.</param>
    /// <returns>The clamped value.</returns>
    public static int Clamp(int value, int min, int max)
    {
        return value > max ? max : value < min ? min : value;
    }
}
