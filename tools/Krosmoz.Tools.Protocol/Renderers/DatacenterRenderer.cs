// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Core.Extensions;
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
        foreach (var d2OField in symbol.D2OClass.Fields)
        {
            if (d2OField.Name[0] is '_')
                d2OField.Name = d2OField.Name[1..];

            d2OField.Name = d2OField.Name.Capitalize();

            if (d2OField.Name.Equals(symbol.D2OClass.Name, StringComparison.OrdinalIgnoreCase))
                d2OField.Name = string.Concat(d2OField.Name, '_');
        }

        var @namespace = string.Concat("Krosmoz.Protocol", '.', string.Join('.', symbol.D2OClass.Namespace.Replace("com.ankamagames.dofus.", string.Empty).Split('.').Select(static x => x.Capitalize())));

        symbol.D2OClass.Namespace = @namespace;

        var className = GetClassName(symbol.D2OClass.Name);
        var isSealed = CanBeSealed(className);

        var builder = new IndentedStringBuilder()
            .AppendLine("// Copyright (c) Krosmoz 2025.")
            .AppendLine("// Krosmoz licenses this file to you under the MIT license.")
            .AppendLine("// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.")
            .AppendLine()
            .AppendLine("namespace {0};", @namespace)
            .AppendLine()
            .AppendLine("public{0}class {1}", isSealed ? " sealed " : ' ', className);

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
                    .AppendIndentedLine("public {0} {1} {{ get; set; }}", fieldType, fieldName)
                    .AppendLine();
            }

            builder.AppendIndentedLine("public static {0} Deserialize(D2OClass d2OClass, BigEndianReader reader)", symbol.D2OClass.Name);

            using (builder.CreateScope())
            {
                builder
                    .AppendIndentedLine("return new()")
                    .AppendIndentedLine('{')
                    .Indent();

                var i = 0;

                foreach (var d2OField in symbol.D2OClass.Fields)
                {
                    switch ((D2OFieldTypes)d2OField.Type)
                    {
                        case D2OFieldTypes.Int:
                            builder.AppendIndentedLine("{0} = d2OClass.Fields[{1}].AsInt(reader),", d2OField.Name, i);
                            break;

                        case D2OFieldTypes.UInt:
                            builder.AppendIndentedLine("{0} = d2OClass.Fields[{1}].AsUInt(reader),", d2OField.Name, i);
                            break;

                        case D2OFieldTypes.Boolean:
                            builder.AppendIndentedLine("{0} = d2OClass.Fields[{1}].AsBoolean(reader),", d2OField.Name, i);
                            break;

                        case D2OFieldTypes.Number:
                            builder.AppendIndentedLine("{0} = d2OClass.Fields[{1}].AsDouble(reader),", d2OField.Name, i);
                            break;

                        case D2OFieldTypes.String:
                            builder.AppendIndentedLine("{0} = d2OClass.Fields[{1}].AsString(reader),", d2OField.Name, i);
                            break;

                        case D2OFieldTypes.I18N:
                            builder.AppendIndentedLine("{0} = d2OClass.Fields[{1}].AsI18N(reader),", d2OField.Name, i);
                            break;

                        case D2OFieldTypes.Vector:
                            if (d2OField.InnerTypeIds!.Count is 2)
                            {
                                var subSubMethod = (D2OFieldTypes)d2OField.InnerTypeIds[1] switch
                                {
                                    D2OFieldTypes.Int => "AsListOfList<int>(reader, static (field, r) => field.AsInt(r))",
                                    D2OFieldTypes.UInt => "AsListOfList<uint>(reader, static (field, r) => field.AsUInt(r))",
                                    D2OFieldTypes.Boolean => "AsListOfList<bool>(reader, static (field, r) => field.AsBoolean(r))",
                                    D2OFieldTypes.Number => "AsListOfList<double>(reader, static (field, r) => field.AsDouble(r))",
                                    D2OFieldTypes.String => "AsListOfList<string>(reader, static (field, r) => field.AsString(r))",
                                    D2OFieldTypes.I18N => "AsListOfList<int>(reader, static (field, r) => field.AsI18N(r))",
                                    _ => $"AsListOfList<{symbol.D2OClasses[symbol.ModuleName][d2OField.InnerTypeIds[1]].Name}>(reader, static (field, r) => field.AsObject<{symbol.D2OClasses[symbol.ModuleName][d2OField.InnerTypeIds[1]].Name}>(r))"
                                };

                                builder.AppendIndentedLine("{0} = d2OClass.Fields[{1}].{2},", d2OField.Name, i, subSubMethod);
                            }
                            else
                            {
                                var subMethod = (D2OFieldTypes)d2OField.InnerTypeIds[0] switch
                                {
                                    D2OFieldTypes.Int => "AsList<int>(reader, static (field, r) => field.AsInt(r))",
                                    D2OFieldTypes.UInt => "AsList<uint>(reader, static (field, r) => field.AsUInt(r))",
                                    D2OFieldTypes.Boolean => "AsList<bool>(reader, static (field, r) => field.AsBoolean(r))",
                                    D2OFieldTypes.Number => "AsList<double>(reader, static (field, r) => field.AsDouble(r))",
                                    D2OFieldTypes.String => "AsList<string>(reader, static (field, r) => field.AsString(r))",
                                    D2OFieldTypes.I18N => "AsList<int>(reader, static (field, r) => field.AsI18N(r))",
                                    _ => $"AsList<{symbol.D2OClasses[symbol.ModuleName][d2OField.InnerTypeIds[0]].Name}>(reader, static (field, r) => field.AsObject<{symbol.D2OClasses[symbol.ModuleName][d2OField.InnerTypeIds[0]].Name}>(r))"
                                };

                                builder.AppendIndentedLine("{0} = d2OClass.Fields[{1}].{2},", d2OField.Name, i, subMethod);
                            }
                            break;

                        default:
                            builder.AppendIndentedLine("{0} = d2OClass.Fields[{1}].AsObject<{2}>(reader),", d2OField.Name, i, symbol.D2OClasses[symbol.ModuleName][d2OField.Type].Name);
                            break;
                    }

                    i++;
                }

                builder
                    .Unindent()
                    .AppendIndentedLine("};");
            }
        }

        return builder.ToString();
    }

    /// <summary>
    /// Gets the class name and its inheritance or interface implementation based on the original name.
    /// </summary>
    /// <param name="originalName">The original name of the class.</param>
    /// <returns>A string representing the class name with its inheritance or interface implementation.</returns>
    private static string GetClassName(string originalName) =>
        originalName switch
        {
            "EffectInstanceInteger" => "EffectInstanceInteger : EffectInstance, IDofusObject<EffectInstanceInteger>",
            "EffectInstanceDice" => "EffectInstanceDice : EffectInstanceInteger, IDofusObject<EffectInstanceDice>",
            "Weapon" => "Weapon : Item, IDofusObject<Weapon>",
            "QuestObjectiveBringItemToNpc" => "QuestObjectiveBringItemToNpc : QuestObjective, IDofusObject<QuestObjectiveBringItemToNpc>",
            "QuestObjectiveBringSoulToNpc" => "QuestObjectiveBringSoulToNpc : QuestObjective, IDofusObject<QuestObjectiveBringSoulToNpc>",
            "QuestObjectiveCraftItem" => "QuestObjectiveCraftItem : QuestObjective, IDofusObject<QuestObjectiveCraftItem>",
            "QuestObjectiveDiscoverMap" => "QuestObjectiveDiscoverMap : QuestObjective, IDofusObject<QuestObjectiveDiscoverMap>",
            "QuestObjectiveDiscoverSubArea" => "QuestObjectiveDiscoverSubArea : QuestObjective, IDofusObject<QuestObjectiveDiscoverSubArea>",
            "QuestObjectiveDuelSpecificPlayer" => "QuestObjectiveDuelSpecificPlayer : QuestObjective, IDofusObject<QuestObjectiveDuelSpecificPlayer>",
            "QuestObjectiveFightMonster" => "QuestObjectiveFightMonster : QuestObjective, IDofusObject<QuestObjectiveFightMonster>",
            "QuestObjectiveFightMonstersOnMap" => "QuestObjectiveFightMonstersOnMap : QuestObjective, IDofusObject<QuestObjectiveFightMonstersOnMap>",
            "QuestObjectiveFreeForm" => "QuestObjectiveFreeForm : QuestObjective, IDofusObject<QuestObjectiveFreeForm>",
            "QuestObjectiveGoToNpc" => "QuestObjectiveGoToNpc : QuestObjective, IDofusObject<QuestObjectiveGoToNpc>",
            "QuestObjectiveMultiFightMonster" => "QuestObjectiveMultiFightMonster : QuestObjective, IDofusObject<QuestObjectiveMultiFightMonster>",
            _ => string.Concat(originalName, $" : IDofusObject<{originalName}>")
        };

    /// <summary>
    /// Determines whether a class can be sealed based on its name.
    /// </summary>
    /// <param name="className">The name of the class.</param>
    /// <returns><c>true</c> if the class can be sealed; otherwise, <c>false</c>.</returns>
    private static bool CanBeSealed(string className)
    {
        return !(className.Contains(": EffectInstanceInteger") ||
                 className.Contains(": EffectInstance") ||
                 className.Contains(": SocialRight") ||
                 className.Contains(": SocialRightGroup") ||
                 className.Contains(": Item") ||
                 className.Contains(": QuestObjective"));
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
                if (field.InnerTypeIds!.Count is 2)
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
