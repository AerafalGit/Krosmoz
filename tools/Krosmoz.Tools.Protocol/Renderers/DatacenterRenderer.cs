// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Core.IO.Text;
using Krosmoz.Serialization.D2O;
using Krosmoz.Tools.Protocol.Models;

namespace Krosmoz.Tools.Protocol.Renderers;

/// <summary>
/// A renderer that generates C# class definitions for datacenter symbols.
/// </summary>
public sealed class DatacenterRenderer : IRenderer<DatacenterSymbol>
{
    /// <summary>
    /// Renders a datacenter symbol into a C# class definition.
    /// </summary>
    /// <param name="symbol">The datacenter symbol to render.</param>
    /// <returns>A string containing the rendered C# class definition.</returns>
    public string Render(DatacenterSymbol symbol)
    {
        var builder = new IndentedStringBuilder()
            .AppendLine("// Copyright (c) Krosmoz 2025.")
            .AppendLine("// Krosmoz licenses this file to you under the MIT license.")
            .AppendLine("// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.")
            .AppendLine()
            .AppendLine("namespace {0};", symbol.D2OClass.Namespace)
            .AppendLine()
            .AppendLine("public sealed class {0} : IDatacenterObject", symbol.D2OClass.Name);

        using (builder.CreateScope())
        {
            builder
                .AppendIndentedLine("public static string ModuleName =>")
                .Indent()
                .AppendIndentedLine("\"{0}\";", symbol.ModuleName)
                .Unindent()
                .AppendLine();

            foreach (var field in symbol.D2OClass.Fields)
            {
                var fieldType = GetFieldType(symbol, field);
                var fieldName = field.Name;

                builder
                    .AppendIndentedLine("public required {0} {1} {{ get; set; }}", fieldType, fieldName)
                    .AppendLine();

                if ((D2OFieldTypes)field.Type is D2OFieldTypes.I18N)
                    builder
                        .AppendIndentedLine("public required string {0} {{ get; set; }}", fieldName.Replace("Id", string.Empty))
                        .AppendLine();
            }

            builder.AppendIndentedLine("public void Deserialize(D2OClass d2OClass, BigEndianReader reader)");

            using (builder.CreateScope())
            {
                foreach (var d2OField in symbol.D2OClass.Fields)
                {
                    switch ((D2OFieldTypes)d2OField.Type)
                    {
                        case D2OFieldTypes.Int:
                            builder.AppendIndentedLine("{0} = d2OClass.ReadFieldAsInt(reader);", d2OField.Name);
                            break;

                        case D2OFieldTypes.UInt:
                            builder.AppendIndentedLine("{0} = d2OClass.ReadFieldAsUInt(reader);", d2OField.Name);
                            break;

                        case D2OFieldTypes.Boolean:
                            builder.AppendIndentedLine("{0} = d2OClass.ReadFieldAsBoolean(reader);", d2OField.Name);
                            break;

                        case D2OFieldTypes.Number:
                            builder.AppendIndentedLine("{0} = d2OClass.ReadFieldAsNumber(reader);", d2OField.Name);
                            break;

                        case D2OFieldTypes.String:
                            builder.AppendIndentedLine("{0} = d2OClass.ReadFieldAsString(reader);", d2OField.Name);
                            break;

                        case D2OFieldTypes.I18N:
                            builder
                                .AppendIndentedLine("{0} = d2OClass.ReadFieldAsI18N(reader);", d2OField.Name)
                                .AppendIndentedLine("{0} = d2OClass.ReadFieldAsI18NString({1});", d2OField.Name.Replace("Id", string.Empty), d2OField.Name);
                            break;

                        case D2OFieldTypes.Vector:
                            if (d2OField.InnerTypeIds.Count is 2)
                            {
                                var subSubMethod = (D2OFieldTypes)d2OField.InnerTypeIds[1] switch
                                {
                                    D2OFieldTypes.Int => "AsListOfList(reader, static (c, r) => c.ReadFieldAsInt(r))",
                                    D2OFieldTypes.UInt => "AsListOfList(reader, static (c, r) => c.ReadFieldAsUInt(r))",
                                    D2OFieldTypes.Boolean => "AsListOfList(reader, static (c, r) => c.ReadFieldAsBoolean(r))",
                                    D2OFieldTypes.Number => "AsListOfList(reader, static (c, r) => c.ReadFieldAsNumber(r))",
                                    D2OFieldTypes.String => "AsListOfList(reader, static (c, r) => c.ReadFieldAsString(r))",
                                    D2OFieldTypes.I18N => "AsListOfList(reader, static (c, r) => c.ReadFieldAsI18N(r))",
                                    _ => $"AsListOfList<{symbol.D2OClasses[symbol.ModuleName][d2OField.InnerTypeIds[1]].Name}>(reader, static (c, r) => c.ReadFieldAsObject<{symbol.D2OClasses[symbol.ModuleName][d2OField.InnerTypeIds[1]].Name}>(r))"
                                };

                                builder.AppendIndentedLine("{0} = d2OClass.ReadField{1};", d2OField.Name, subSubMethod);
                            }
                            else
                            {
                                var subMethod = (D2OFieldTypes)d2OField.InnerTypeIds[0] switch
                                {
                                    D2OFieldTypes.Int => "AsList(reader, static (c, r) => c.ReadFieldAsInt(r))",
                                    D2OFieldTypes.UInt => "AsList(reader, static (c, r) => c.ReadFieldAsUInt(r))",
                                    D2OFieldTypes.Boolean => "AsList(reader, static (c, r) => c.ReadFieldAsBoolean(r))",
                                    D2OFieldTypes.Number => "AsList(reader, static (c, r) => c.ReadFieldAsNumber(r))",
                                    D2OFieldTypes.String => "AsList(reader, static (c, r) => c.ReadFieldAsString(r))",
                                    D2OFieldTypes.I18N => "AsList(reader, static (c, r) => c.ReadFieldAsI18N(r))",
                                    _ => $"AsList<{symbol.D2OClasses[symbol.ModuleName][d2OField.InnerTypeIds[0]].Name}>(reader, static (c, r) => c.ReadFieldAsObject<{symbol.D2OClasses[symbol.ModuleName][d2OField.InnerTypeIds[0]].Name}>(r))"
                                };

                                builder.AppendIndentedLine("{0} = d2OClass.ReadField{1};", d2OField.Name, subMethod);
                            }
                            break;

                        default:
                            builder.AppendIndentedLine("{0} = d2OClass.ReadFieldAsObject<{1}>(reader);", d2OField.Name, symbol.D2OClasses[symbol.ModuleName][d2OField.Type].Name);
                            break;
                    }
                }
            }

            builder
                .AppendLine()
                .AppendIndentedLine("public void Serialize(D2OClass d2OClass, BigEndianWriter writer)");

            using (builder.CreateScope())
            {
                foreach (var d2OField in symbol.D2OClass.Fields)
                {
                    switch ((D2OFieldTypes)d2OField.Type)
                    {
                        case D2OFieldTypes.Int:
                            builder.AppendIndentedLine("d2OClass.WriteFieldAsInt(writer, {0});", d2OField.Name);
                            break;

                        case D2OFieldTypes.UInt:
                            builder.AppendIndentedLine("d2OClass.WriteFieldAsUInt(writer, {0});", d2OField.Name);
                            break;

                        case D2OFieldTypes.Boolean:
                            builder.AppendIndentedLine("d2OClass.WriteFieldAsBoolean(writer, {0});", d2OField.Name);
                            break;

                        case D2OFieldTypes.Number:
                            builder.AppendIndentedLine("d2OClass.WriteFieldAsNumber(writer, {0});", d2OField.Name);
                            break;

                        case D2OFieldTypes.String:
                            builder.AppendIndentedLine("d2OClass.WriteFieldAsString(writer, {0});", d2OField.Name);
                            break;

                        case D2OFieldTypes.I18N:
                            builder
                                .AppendIndentedLine("d2OClass.WriteFieldAsI18N(writer, {0});", d2OField.Name);
                            break;

                        case D2OFieldTypes.Vector:
                            if (d2OField.InnerTypeIds.Count is 2)
                            {
                                var subSubMethod = (D2OFieldTypes)d2OField.InnerTypeIds[1] switch
                                {
                                    D2OFieldTypes.Int => $"AsListOfList(writer, {d2OField.Name}, static (c, r, v) => c.WriteFieldAsInt(r, v))",
                                    D2OFieldTypes.UInt => $"AsListOfList(writer, {d2OField.Name}, static (c, r, v) => c.WriteFieldAsUInt(r, v))",
                                    D2OFieldTypes.Boolean => $"AsListOfList(writer, {d2OField.Name}, static (c, r, v) => c.WriteFieldAsBoolean(r, v))",
                                    D2OFieldTypes.Number => $"AsListOfList(writer, {d2OField.Name}, static (c, r, v) => c.WriteFieldAsNumber(r, v))",
                                    D2OFieldTypes.String => $"AsListOfList(writer, {d2OField.Name}, static (c, r, v) => c.WriteFieldAsString(r, v))",
                                    D2OFieldTypes.I18N => $"AsListOfList(writer, {d2OField.Name}, static (c, r, v) => c.WriteFieldAsI18N(r, v))",
                                    _ => $"AsListOfList<{symbol.D2OClasses[symbol.ModuleName][d2OField.InnerTypeIds[1]].Name}>(writer, {d2OField.Name}, static (c, r, v) => c.WriteFieldAsObject<{symbol.D2OClasses[symbol.ModuleName][d2OField.InnerTypeIds[1]].Name}>(r, v))"
                                };

                                builder.AppendIndentedLine("d2OClass.WriteField{0};", subSubMethod);
                            }
                            else
                            {
                                var subMethod = (D2OFieldTypes)d2OField.InnerTypeIds[0] switch
                                {
                                    D2OFieldTypes.Int => $"AsList(writer, {d2OField.Name}, static (c, r, v) => c.WriteFieldAsInt(r, v))",
                                    D2OFieldTypes.UInt => $"AsList(writer, {d2OField.Name}, static (c, r, v) => c.WriteFieldAsUInt(r, v))",
                                    D2OFieldTypes.Boolean => $"AsList(writer, {d2OField.Name}, static (c, r, v) => c.WriteFieldAsBoolean(r, v))",
                                    D2OFieldTypes.Number => $"AsList(writer, {d2OField.Name}, static (c, r, v) => c.WriteFieldAsNumber(r, v))",
                                    D2OFieldTypes.String => $"AsList(writer, {d2OField.Name}, static (c, r, v) => c.WriteFieldAsString(r, v))",
                                    D2OFieldTypes.I18N => $"AsList(writer, {d2OField.Name}, static (c, r, v) => c.WriteFieldAsI18N(r, v))",
                                    _ => $"AsList<{symbol.D2OClasses[symbol.ModuleName][d2OField.InnerTypeIds[0]].Name}>(writer, {d2OField.Name}, static (c, r, v) => c.WriteFieldAsObject<{symbol.D2OClasses[symbol.ModuleName][d2OField.InnerTypeIds[0]].Name}>(r, v))"
                                };

                                builder.AppendIndentedLine("d2OClass.WriteField{0};", subMethod);
                            }
                            break;

                        default:
                            builder.AppendIndentedLine("d2OClass.WriteFieldAsObject<{1}>(writer, {0});", d2OField.Name, symbol.D2OClasses[symbol.ModuleName][d2OField.Type].Name);
                            break;
                    }
                }
            }
        }

        return builder.ToString();
    }

    /// <summary>
    /// Gets the field type as a string based on the field's type and inner type identifiers.
    /// </summary>
    /// <param name="symbol">The datacenter symbol containing the field.</param>
    /// <param name="field">The field whose type is to be determined.</param>
    /// <returns>A string representing the field type.</returns>
    private static string GetFieldType(DatacenterSymbol symbol, D2OField field)
    {
        switch ((D2OFieldTypes)field.Type)
        {
            case D2OFieldTypes.Int:
                return "int";

            case D2OFieldTypes.UInt:
                return "uint";

            case D2OFieldTypes.Boolean:
                return "bool";

            case D2OFieldTypes.Number:
                return "double";

            case D2OFieldTypes.String:
                return "string";

            case D2OFieldTypes.I18N:
                return "int";

            case D2OFieldTypes.Vector:
                if (field.InnerTypeIds.Count is 2)
                {
                    var subSubType = (D2OFieldTypes)field.InnerTypeIds[1] switch
                    {
                        D2OFieldTypes.Int => "int",
                        D2OFieldTypes.UInt => "uint",
                        D2OFieldTypes.Boolean => "bool",
                        D2OFieldTypes.Number => "double",
                        D2OFieldTypes.String => "string",
                        D2OFieldTypes.I18N => "int",
                        _ => symbol.D2OClasses[symbol.ModuleName][field.InnerTypeIds[1]].Name
                    };
                    return $"List<List<{subSubType}>>";
                }

                var subType = (D2OFieldTypes)field.InnerTypeIds[0] switch
                {
                    D2OFieldTypes.Int => "int",
                    D2OFieldTypes.UInt => "uint",
                    D2OFieldTypes.Boolean => "bool",
                    D2OFieldTypes.Number => "double",
                    D2OFieldTypes.String => "string",
                    D2OFieldTypes.I18N => "int",
                    _ => symbol.D2OClasses[symbol.ModuleName][field.InnerTypeIds[0]].Name
                };
                return $"List<{subType}>";

            default:
                return symbol.D2OClasses[symbol.ModuleName][field.Type].Name;
        }
    }
}
