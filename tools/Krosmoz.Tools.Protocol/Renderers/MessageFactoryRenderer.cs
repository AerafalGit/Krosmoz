// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Core.IO.Text;
using Krosmoz.Tools.Protocol.Models;

namespace Krosmoz.Tools.Protocol.Renderers;

/// <summary>
/// Represents a renderer for generating a message factory class based on an array of class symbols.
/// Implements the <see cref="IRenderer{T}"/> interface for rendering <see cref="ClassSymbol"/> arrays.
/// </summary>
public sealed class MessageFactoryRenderer : IRenderer<ClassSymbol[]>
{
    /// <summary>
    /// Renders a message factory class as a string based on the provided class symbols.
    /// </summary>
    /// <param name="symbol">An array of class symbols used to generate the message factory.</param>
    /// <returns>A string containing the generated message factory class.</returns>
    public string Render(ClassSymbol[] symbol)
    {
        var builder = new IndentedStringBuilder()
            .AppendLine("// Copyright (c) Krosmoz 2025.")
            .AppendLine("// Krosmoz licenses this file to you under the MIT license.")
            .AppendLine("// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.")
            .AppendLine()
            .AppendLine("using Krosmoz.Core.Network.Factory;")
            .AppendLine()
            .AppendLine("namespace Krosmoz.Protocol.Messages;")
            .AppendLine()
            .AppendLine("/// <summary>")
            .AppendLine("/// A factory class for creating network messages based on their protocol IDs.")
            .AppendLine("/// </summary>")
            .AppendLine("public sealed class MessageFactory : IMessageFactory<uint, DofusMessage>");

        using (builder.CreateScope())
        {
            builder
                .AppendIndentedLine("/// <summary>")
                .AppendIndentedLine("/// Creates a network message based on the specified message ID.")
                .AppendIndentedLine("/// </summary>")
                .AppendIndentedLine("/// <param name=\"messageId\">The ID of the message to create.</param>")
                .AppendIndentedLine("/// <returns>The created network message.</returns>")
                .AppendIndentedLine("public DofusMessage CreateMessage(uint messageId)");

            using (builder.CreateScope())
            {
                builder
                    .AppendIndentedLine("return messageId switch")
                    .AppendIndentedLine('{')
                    .Indent();

                foreach (var classSymbol in symbol)
                    builder.AppendIndentedLine("{0}.StaticProtocolId => {0}.Empty,", string.Concat(classSymbol.Metadata.Namespace, '.', classSymbol.Metadata.Name));

                builder
                    .AppendIndentedLine("_ => throw new ArgumentOutOfRangeException(nameof(messageId))")
                    .Unindent()
                    .AppendIndentedLine("};");
            }

            builder
                .AppendLine()
                .AppendIndentedLine("/// <summary>")
                .AppendIndentedLine("/// Creates the name of a network message based on the specified message ID.")
                .AppendIndentedLine("/// </summary>")
                .AppendIndentedLine("/// <param name=\"messageId\">The ID of the message to create the name for.</param>")
                .AppendIndentedLine("/// <returns>The name of the created network message.</returns>")
                .AppendIndentedLine("public string CreateMessageName(uint messageId)");

            using (builder.CreateScope())
            {
                builder
                    .AppendIndentedLine("return messageId switch")
                    .AppendIndentedLine('{')
                    .Indent();

                foreach (var classSymbol in symbol)
                    builder.AppendIndentedLine("{0}.StaticProtocolId => nameof({0}),", string.Concat(classSymbol.Metadata.Namespace, '.', classSymbol.Metadata.Name));

                builder
                    .AppendIndentedLine("_ => throw new ArgumentOutOfRangeException(nameof(messageId))")
                    .Unindent()
                    .AppendIndentedLine("};");
            }
        }

        return builder.ToString();
    }
}
