// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Core.IO.Text;
using Krosmoz.Serialization.D2O;

namespace Krosmoz.Tools.Protocol.Renderers;

/// <summary>
/// A renderer that generates a factory class for creating datacenter objects based on their names.
/// </summary>
public sealed class DatacenterObjectFactoryRenderer : IRenderer<Dictionary<string, Dictionary<int, D2OClass>>>
{
    /// <summary>
    /// Renders the factory class definition as a string for the specified datacenter symbol.
    /// </summary>
    /// <param name="symbols">The datacenter symbol containing the class and field definitions.</param>
    /// <returns>A string representing the generated factory class.</returns>
    public string Render(Dictionary<string, Dictionary<int, D2OClass>> symbols)
    {
        var builder = new IndentedStringBuilder()
            .AppendLine("// Copyright (c) Krosmoz 2025.")
            .AppendLine("// Krosmoz licenses this file to you under the MIT license.")
            .AppendLine("// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.")
            .AppendLine()
            .AppendLine("namespace Krosmoz.Protocol.Datacenter;")
            .AppendLine()
            .AppendLine("/// <summary>")
            .AppendLine("/// A factory class responsible for creating datacenter objects based on their names.")
            .AppendLine("/// </summary>")
            .AppendLine("public sealed class DatacenterObjectFactory : IDatacenterObjectFactory");

        using (builder.CreateScope())
        {
            builder
                .AppendIndentedLine("/// <summary>")
                .AppendIndentedLine("/// Creates a datacenter object based on the specified name.")
                .AppendIndentedLine("/// </summary>")
                .AppendIndentedLine("/// <param name=\"name\">The name of the datacenter object to create.</param>")
                .AppendIndentedLine("/// <returns>")
                .AppendIndentedLine("/// An instance of <see cref=\"IDatacenterObject\"/> representing the created object.")
                .AppendIndentedLine("/// Throws an exception if the specified name does not match any known datacenter object type.")
                .AppendIndentedLine("/// </returns>")
                .AppendIndentedLine("public IDatacenterObject CreateInstance(string name)");

            using (builder.CreateScope())
            {
                builder
                    .AppendIndentedLine("return name switch")
                    .AppendIndentedLine('{')
                    .Indent();

                foreach (var (moduleName, d2OClasses) in symbols.Select(static x => (x.Key, x.Value.Select(static y => y.Value))))
                {
                    foreach (var d2OClass in d2OClasses)
                    {
                        builder.AppendIndented("nameof({0}.{1}) => new {0}.{1} {{ ", d2OClass.Namespace, d2OClass.Name);

                        foreach (var d2OField in d2OClass.Fields)
                        {
                            var defaultValue = (D2OFieldTypes)d2OField.Type switch
                            {
                                D2OFieldTypes.Int => "0",
                                D2OFieldTypes.Boolean => "false",
                                D2OFieldTypes.String => "string.Empty",
                                D2OFieldTypes.Number => "0",
                                D2OFieldTypes.I18N => "0",
                                D2OFieldTypes.UInt => "0",
                                D2OFieldTypes.Vector => "[]",
                                _ => $"({symbols[moduleName][d2OField.Type].Namespace.Replace("Krosmoz.Protocol.Datacenter.", string.Empty)}.{symbols[moduleName][d2OField.Type].Name})CreateInstance(\"{symbols[moduleName][d2OField.Type].Name}\")"
                            };

                            builder.Append("{0} = {1}, ", d2OField.Name, defaultValue);

                            if ((D2OFieldTypes)d2OField.Type is D2OFieldTypes.I18N)
                                builder.Append("{0} = string.Empty, ", d2OField.Name.Replace("Id", string.Empty));
                        }

                        builder.AppendLine("},");
                    }
                }

                builder
                    .AppendIndentedLine("_ => throw new Exception($\"Unknown datacenter object name: {name}\")")
                    .Unindent()
                    .AppendIndentedLine("};");
            }
        }

        return builder.ToString();
    }
}
