// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Krosmoz.Core.IO.Text;

/// <summary>
/// Represents a disposable action that executes a specified <see cref="Action"/> when disposed.
/// </summary>
file sealed record DisposableAction(Action Action) : IDisposable
{
    /// <summary>
    /// Executes the action when the object is disposed.
    /// </summary>
    public void Dispose()
    {
        Action();
    }
}

/// <summary>
/// Provides functionality to build strings with indentation support.
/// </summary>
[DebuggerDisplay("{ToString(),nq}")]
public sealed class IndentedStringBuilder
{
    private readonly StringBuilder _builder;
    private int _indentation;

    /// <summary>
    /// Initializes a new instance of the <see cref="IndentedStringBuilder"/> class.
    /// </summary>
    public IndentedStringBuilder()
    {
        _builder = new StringBuilder();
    }

    /// <summary>
    /// Increases the indentation level by one.
    /// </summary>
    /// <returns>The current <see cref="IndentedStringBuilder"/> instance.</returns>
    public IndentedStringBuilder Indent()
    {
        _indentation++;
        return this;
    }

    /// <summary>
    /// Decreases the indentation level by one, if greater than zero.
    /// </summary>
    /// <returns>The current <see cref="IndentedStringBuilder"/> instance.</returns>
    public IndentedStringBuilder Unindent()
    {
        if (_indentation > 0)
            _indentation--;
        return this;
    }

    /// <summary>
    /// Appends a single character to the builder.
    /// </summary>
    /// <param name="value">The character to append.</param>
    /// <returns>The current <see cref="IndentedStringBuilder"/> instance.</returns>
    public IndentedStringBuilder Append(char value)
    {
        _builder.Append(value);
        return this;
    }

    /// <summary>
    /// Appends a string to the builder.
    /// </summary>
    /// <param name="value">The string to append.</param>
    /// <returns>The current <see cref="IndentedStringBuilder"/> instance.</returns>
    public IndentedStringBuilder Append(string value)
    {
        _builder.Append(value);
        return this;
    }

    /// <summary>
    /// Appends a formatted string to the builder.
    /// </summary>
    /// <param name="value">The composite format string.</param>
    /// <param name="args">The arguments to format.</param>
    /// <returns>The current <see cref="IndentedStringBuilder"/> instance.</returns>
    public IndentedStringBuilder Append([StringSyntax(StringSyntaxAttribute.CompositeFormat)] string value, params object[] args)
    {
        _builder.AppendFormat(value, args);
        return this;
    }

    /// <summary>
    /// Appends a single character with the current indentation.
    /// </summary>
    /// <param name="value">The character to append.</param>
    /// <returns>The current <see cref="IndentedStringBuilder"/> instance.</returns>
    public IndentedStringBuilder AppendIndented(char value)
    {
        _builder
            .Append(new string('\t', _indentation))
            .Append(value);
        return this;
    }

    /// <summary>
    /// Appends a string with the current indentation.
    /// </summary>
    /// <param name="value">The string to append.</param>
    /// <returns>The current <see cref="IndentedStringBuilder"/> instance.</returns>
    public IndentedStringBuilder AppendIndented(string value)
    {
        _builder
            .Append(new string('\t', _indentation))
            .Append(value);
        return this;
    }

    /// <summary>
    /// Appends a formatted string with the current indentation.
    /// </summary>
    /// <param name="value">The composite format string.</param>
    /// <param name="args">The arguments to format.</param>
    /// <returns>The current <see cref="IndentedStringBuilder"/> instance.</returns>
    public IndentedStringBuilder AppendIndented([StringSyntax(StringSyntaxAttribute.CompositeFormat)] string value, params object[] args)
    {
        _builder
            .Append(new string('\t', _indentation))
            .AppendFormat(value, args);
        return this;
    }

    /// <summary>
    /// Appends a single character with the current indentation and a new line.
    /// </summary>
    /// <param name="value">The character to append.</param>
    /// <returns>The current <see cref="IndentedStringBuilder"/> instance.</returns>
    public IndentedStringBuilder AppendIndentedLine(char value)
    {
        _builder
            .Append(new string('\t', _indentation))
            .Append(value)
            .AppendLine();
        return this;
    }

    /// <summary>
    /// Appends a string with the current indentation and a new line.
    /// </summary>
    /// <param name="value">The string to append.</param>
    /// <returns>The current <see cref="IndentedStringBuilder"/> instance.</returns>
    public IndentedStringBuilder AppendIndentedLine(string value)
    {
        _builder
            .Append(new string('\t', _indentation))
            .AppendLine(value);
        return this;
    }

    /// <summary>
    /// Appends a formatted string with the current indentation and a new line.
    /// </summary>
    /// <param name="value">The composite format string.</param>
    /// <param name="args">The arguments to format.</param>
    /// <returns>The current <see cref="IndentedStringBuilder"/> instance.</returns>
    public IndentedStringBuilder AppendIndentedLine([StringSyntax(StringSyntaxAttribute.CompositeFormat)] string value, params object[] args)
    {
        _builder
            .Append(new string('\t', _indentation))
            .AppendFormat(value, args)
            .AppendLine();
        return this;
    }

    /// <summary>
    /// Appends a new line to the builder.
    /// </summary>
    /// <returns>The current <see cref="IndentedStringBuilder"/> instance.</returns>
    public IndentedStringBuilder AppendLine()
    {
        _builder.AppendLine();
        return this;
    }

    /// <summary>
    /// Appends a single character followed by a new line.
    /// </summary>
    /// <param name="value">The character to append.</param>
    /// <returns>The current <see cref="IndentedStringBuilder"/> instance.</returns>
    public IndentedStringBuilder AppendLine(char value)
    {
        _builder
            .Append(value)
            .AppendLine();
        return this;
    }

    /// <summary>
    /// Appends a string followed by a new line.
    /// </summary>
    /// <param name="value">The string to append.</param>
    /// <returns>The current <see cref="IndentedStringBuilder"/> instance.</returns>
    public IndentedStringBuilder AppendLine(string value)
    {
        _builder.AppendLine(value);
        return this;
    }

    /// <summary>
    /// Appends a formatted string followed by a new line.
    /// </summary>
    /// <param name="value">The composite format string.</param>
    /// <param name="args">The arguments to format.</param>
    /// <returns>The current <see cref="IndentedStringBuilder"/> instance.</returns>
    public IndentedStringBuilder AppendLine([StringSyntax(StringSyntaxAttribute.CompositeFormat)] string value, params object[] args)
    {
        _builder
            .AppendFormat(value, args)
            .AppendLine();
        return this;
    }

    /// <summary>
    /// Clears the builder and resets the indentation level.
    /// </summary>
    /// <returns>The current <see cref="IndentedStringBuilder"/> instance.</returns>
    public IndentedStringBuilder Clear()
    {
        _builder.Clear();
        _indentation = 0;
        return this;
    }

    /// <summary>
    /// Creates a scope by appending an opening brace, increasing the indentation,
    /// and returning a disposable action to close the scope.
    /// </summary>
    /// <returns>An <see cref="IDisposable"/> that closes the scope when disposed.</returns>
    public IDisposable CreateScope()
    {
        _builder
            .Append(new string('\t', _indentation))
            .AppendLine("{");

        _indentation++;

        return new DisposableAction(() =>
        {
            if (_indentation > 0)
                _indentation--;

            _builder
                .Append(new string('\t', _indentation))
                .AppendLine("}");
        });
    }

    /// <summary>
    /// Returns the string representation of the builder's content.
    /// </summary>
    /// <returns>The string representation of the builder's content.</returns>
    public override string ToString()
    {
        return _builder.ToString();
    }
}
