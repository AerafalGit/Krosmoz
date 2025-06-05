// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Core.IO.Text;
using Krosmoz.Tools.Protocol.Models;

namespace Krosmoz.Tools.Protocol.Renderers;

/// <summary>
/// Provides functionality to render enumeration symbols into a string representation.
/// </summary>
public sealed class EnumRenderer : IRenderer<EnumSymbol>
{
    /// <summary>
    /// Renders the provided enumeration symbol into a formatted string representation.
    /// </summary>
    /// <param name="symbol">The enumeration symbol to render.</param>
    /// <returns>A string representation of the rendered enumeration symbol.</returns>
    public string Render(EnumSymbol symbol)
    {
        var builder = new IndentedStringBuilder()
            .AppendLine("// Copyright (c) Krosmoz 2025.")
            .AppendLine("// Krosmoz licenses this file to you under the MIT license.")
            .AppendLine("// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.")
            .AppendLine()
            .AppendLine("namespace Krosmoz.Protocol.Enums;")
            .AppendLine();

        builder.AppendLine("public enum {0}", symbol.Metadata.Name);

        using (builder.CreateScope())
        {
            foreach (var (index, property) in symbol.Properties.Index())
                builder.AppendIndentedLine("{0} = {1}{2}", property.Name, property.Value, index == symbol.Properties.Count - 1 ? string.Empty : ",");
        }

        return builder.ToString();
    }
}
