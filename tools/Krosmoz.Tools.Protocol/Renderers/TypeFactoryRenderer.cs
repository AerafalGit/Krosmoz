// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Core.IO.Text;
using Krosmoz.Tools.Protocol.Models;

namespace Krosmoz.Tools.Protocol.Renderers;

/// <summary>
/// Represents a renderer for generating a type factory class based on an array of class symbols.
/// Implements the <see cref="IRenderer{T}"/> interface for rendering <see cref="ClassSymbol"/> arrays.
/// </summary>
public sealed class TypeFactoryRenderer : IRenderer<ClassSymbol[]>
{
    /// <summary>
    /// Renders a type factory class as a string based on the provided class symbols.
    /// </summary>
    /// <param name="symbol">An array of class symbols used to generate the type factory.</param>
    /// <returns>A string containing the generated type factory class.</returns>
    public string Render(ClassSymbol[] symbol)
    {
        var builder = new IndentedStringBuilder()
            .AppendLine("// Copyright (c) Krosmoz 2025.")
            .AppendLine("// Krosmoz licenses this file to you under the MIT license.")
            .AppendLine("// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.")
            .AppendLine()
            .AppendLine("using Krosmoz.Core.Network.Protocol.Dofus;")
            .AppendLine()
            .AppendLine("namespace Krosmoz.Protocol.Types;")
            .AppendLine()
            .AppendLine("public static class TypeFactory");

        using (builder.CreateScope())
        {
            builder
                .AppendIndentedLine("public T CreateType<T>(ushort typeId)")
                .Indent()
                .AppendIndentedLine("where T : DofusType")
                .Unindent();

            using (builder.CreateScope())
            {
                builder
                    .AppendIndentedLine("return typeId switch")
                    .AppendIndentedLine('{')
                    .Indent();

                foreach (var classSymbol in symbol)
                    builder.AppendIndentedLine("{0} => (T){1}.Empty,", classSymbol.ProtocolId, string.Concat(classSymbol.Metadata.Namespace, '.', classSymbol.Metadata.Name));

                builder
                    .AppendIndentedLine("_ => throw new ArgumentOutOfRangeException(nameof(typeId))")
                    .Unindent()
                    .AppendIndentedLine("};");
            }
        }

        return builder.ToString();
    }
}
