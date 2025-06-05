// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Serialization.D2I;

/// <summary>
/// Represents a text entry in a D2I file.
/// </summary>
/// <typeparam name="T">The type of the identifier, which must be non-nullable.</typeparam>
public sealed class D2IText<T>
    where T : notnull
{
    /// <summary>
    /// Gets or sets the identifier of the text entry.
    /// </summary>
    public T Id { get; set; }

    /// <summary>
    /// Gets or sets the main text associated with the identifier.
    /// </summary>
    public string Text { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the non-diacritical text should be used.
    /// </summary>
    public bool UseNotDiacriticalText { get; set; }

    /// <summary>
    /// Gets or sets the non-diacritical version of the text.
    /// </summary>
    public string NotDiacriticalText { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="D2IText{T}"/> class with the specified identifier and text.
    /// </summary>
    /// <param name="id">The identifier of the text entry.</param>
    /// <param name="text">The main text associated with the identifier.</param>
    public D2IText(T id, string text)
    {
        Id = id;
        Text = text;
        NotDiacriticalText = string.Empty;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="D2IText{T}"/> class with the specified identifier, text, and non-diacritical text.
    /// </summary>
    /// <param name="id">The identifier of the text entry.</param>
    /// <param name="text">The main text associated with the identifier.</param>
    /// <param name="notDiacriticalText">The non-diacritical version of the text.</param>
    public D2IText(T id, string text, string notDiacriticalText)
    {
        Id = id;
        Text = text;
        UseNotDiacriticalText = true;
        NotDiacriticalText = notDiacriticalText;
    }
}
