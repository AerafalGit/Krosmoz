// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using System.Diagnostics;
using Krosmoz.Core.Extensions;
using Krosmoz.Core.IO.Text;
using Krosmoz.Tools.Protocol.Extensions;
using Krosmoz.Tools.Protocol.Models;
using Krosmoz.Tools.Protocol.Storages.Symbols;

namespace Krosmoz.Tools.Protocol.Renderers;

/// <summary>
/// Renders a class symbol into a string representation.
/// </summary>
public sealed class ClassRenderer : IRenderer<ClassSymbol>
{
    private readonly ISymbolStorage _symbolStorage;

    /// <summary>
    /// Initializes a new instance of the <see cref="ClassRenderer"/> class.
    /// </summary>
    /// <param name="symbolStorage">The symbol storage used to retrieve symbols.</param>
    public ClassRenderer(ISymbolStorage symbolStorage)
    {
        _symbolStorage = symbolStorage;
    }

    /// <summary>
    /// Renders the specified class symbol into a string.
    /// </summary>
    /// <param name="symbol">The class symbol to render.</param>
    /// <returns>A string representation of the class symbol.</returns>
    public string Render(ClassSymbol symbol)
    {
        var canBeSealed = _symbolStorage.GetSymbols().All(x => x.Metadata.ParentName != symbol.Metadata.Name);

        var builder = new IndentedStringBuilder()
            .AppendLine("// Copyright (c) Krosmoz 2025.")
            .AppendLine("// Krosmoz licenses this file to you under the MIT license.")
            .AppendLine("// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.")
            .AppendLine();

        RenderUsings(builder, symbol);

        builder
            .AppendLine("namespace {0};", symbol.Metadata.Namespace)
            .AppendLine()
            .AppendLine("public {0}class {1} : {2}", canBeSealed ? "sealed " : string.Empty, symbol.Metadata.Name, symbol.Metadata.ParentName);

        using (builder.CreateScope())
        {
            var protocolIdPropertyType = symbol.Metadata.Kind is SymbolKind.Type ? "ushort" : "uint";

            builder
                .AppendIndentedLine("public new const {0} StaticProtocolId = {1};", protocolIdPropertyType, symbol.ProtocolId)
                .AppendLine()
                .AppendIndentedLine("public override {0} ProtocolId =>", protocolIdPropertyType)
                .Indent()
                .AppendIndentedLine("StaticProtocolId;")
                .Unindent()
                .AppendLine();

            RenderFactoryProperty(builder, symbol);

            if (symbol.Properties.Count is not 0 || symbol.Metadata.Name is "NetworkDataContainerMessage")
                builder.AppendLine();

            RenderProperties(builder, symbol);
            RenderSerializeMethod(builder, symbol);

            if (symbol.Properties.Count is not 0 || symbol.Metadata.Name is "NetworkDataContainerMessage")
                builder.AppendLine();

            RenderDeserializeMethod(builder, symbol);
        }

        return builder.ToString();
    }

    /// <summary>
    /// Retrieves all properties of the specified class symbol, including those inherited from its parent classes.
    /// </summary>
    /// <param name="symbol">The class symbol whose properties are to be retrieved.</param>
    /// <returns>An enumerable collection of <see cref="PropertySymbol"/> objects.</returns>
    private IEnumerable<PropertySymbol> GetRecursiveProperties(ClassSymbol symbol)
    {
        var parentProperties = new List<PropertySymbol>();

        if (HaveCustomParent(symbol))
        {
            for (var currentSymbol = symbol; _symbolStorage.TryGetSymbol(currentSymbol.Metadata.ParentName, out currentSymbol);)
                parentProperties.AddRange(currentSymbol.Properties.Values);
        }

        parentProperties.Reverse();

        return parentProperties.Concat(symbol.Properties.Values);
    }

    /// <summary>
    /// Renders the necessary using directives for the specified class symbol.
    /// </summary>
    /// <param name="builder">The <see cref="IndentedStringBuilder"/> used to build the output.</param>
    /// <param name="symbol">The class symbol for which the using directives are rendered.</param>
    private static void RenderUsings(IndentedStringBuilder builder, ClassSymbol symbol)
    {
        if (symbol.Usings.Count is 0)
            return;

        var hasUsings = false;

        foreach (var @using in symbol.Usings.Distinct())
        {
            if (@using.EndsWith("Version"))
            {
                builder.AppendIndentedLine("using Version = Krosmoz.Protocol.Types.Version.Version;");
                hasUsings = true;
                continue;
            }

            var usingNs = string.Join('.', @using.Replace("com.ankamagames.dofus.network.", string.Empty).Split('.')[..^1].Select<string, string>(static x => x.Capitalize()));

            if (symbol.Metadata.Namespace.Equals(usingNs, StringComparison.InvariantCulture))
                continue;

            builder.AppendIndentedLine("using {0};", string.Concat("Krosmoz.Protocol.", usingNs));
            hasUsings = true;
        }

        if (hasUsings)
            builder.AppendLine();
    }

    /// <summary>
    /// Renders the factory property for the specified class symbol.
    /// </summary>
    /// <param name="builder">The <see cref="IndentedStringBuilder"/> used to build the output.</param>
    /// <param name="symbol">The class symbol for which the factory property is rendered.</param>
    private void RenderFactoryProperty(IndentedStringBuilder builder, ClassSymbol symbol)
    {
        if (HaveCustomParent(symbol))
            builder.AppendIndentedLine("public static new {0} Empty =>", symbol.Metadata.Name);
        else
            builder.AppendIndentedLine("public static {0} Empty =>", symbol.Metadata.Name);

        builder.Indent();

        var properties = GetRecursiveProperties(symbol).ToArray();

        if (properties.Length is 0)
        {
            builder
                .AppendIndentedLine("new();")
                .Unindent();
            return;
        }

        builder.AppendIndented("new() {");

        foreach (var (index, property) in properties.Index())
        {
            var comma = index == properties.Length - 1 ? string.Empty : ",";

            switch (property.PropertyKind)
            {
                case PropertyKind.Primitive:
                    builder.Append(" {0} = {1}{2}", property.Name, property.ObjectType switch
                    {
                        "string" => "string.Empty",
                        "bool" => "false",
                        "byte[]" => "[]",
                        _ => "0"
                    }, comma);
                    break;

                case PropertyKind.Vector:
                    builder.Append(" {0} = []{1}", property.Name, comma);
                    break;

                case PropertyKind.Object:
                    Debug.Assert(property.ObjectType is not null);
                    builder.Append(" {0} = {1}.Empty{2}", property.Name, property.ObjectType, comma);
                    break;

                default:
                    throw new Exception($"Unknown property kind for property {property.Name}.");
            }
        }

        builder
            .AppendLine(" };")
            .Unindent();
    }

    /// <summary>
    /// Renders the properties of the specified class symbol.
    /// </summary>
    /// <param name="builder">The <see cref="IndentedStringBuilder"/> used to build the output.</param>
    /// <param name="symbol">The class symbol whose properties are to be rendered.</param>
    private static void RenderProperties(IndentedStringBuilder builder, ClassSymbol symbol)
    {
        foreach (var property in symbol.Properties.Values)
        {
            Debug.Assert(property.ObjectType is not null);

            switch (property.PropertyKind)
            {
                case PropertyKind.Primitive:
                case PropertyKind.Object:
                    builder.AppendIndentedLine("public required {0} {1} {{ get; set; }}", property.ObjectType, property.Name);
                    break;

                case PropertyKind.Vector:
                    builder.AppendIndentedLine("public required IEnumerable<{0}> {1} {{ get; set; }}", property.ObjectType, property.Name);
                    break;
            }

            builder.AppendLine();
        }
    }

    /// <summary>
    /// Renders the serialization logic for the properties of the specified class symbol.
    /// </summary>
    /// <param name="builder">The <see cref="IndentedStringBuilder"/> used to build the output.</param>
    /// <param name="symbol">The class symbol whose properties are to be serialized.</param>
    private static void RenderSerializeMethod(IndentedStringBuilder builder, ClassSymbol symbol)
    {
        if (symbol.Properties.Count is 0)
            return;

        builder.AppendIndentedLine("public override void Serialize(BigEndianWriter writer)");

        using (builder.CreateScope())
        {
            if (HaveCustomParent(symbol))
                builder.AppendIndentedLine("base.Serialize(writer);");

            var flags = symbol.Properties.Values.Count(static x => x.MethodKind is MethodKind.BooleanByteWrapper);
            var flagsIndex = 0;

            if (flags > 0)
                builder.AppendIndentedLine("var flag = new byte();");

            foreach (var property in symbol.Properties.Values)
            {
                switch (property.PropertyKind)
                {
                    case PropertyKind.Primitive:
                        switch (property.MethodKind)
                        {
                            case MethodKind.Primitive:
                                Debug.Assert(property.WriteMethod is not null);
                                builder.AppendIndentedLine("writer.{0}({1});", property.WriteMethod, property.Name);
                                break;

                            case MethodKind.BooleanByteWrapper:
                                Debug.Assert(property.WriteMethod is not null);
                                if (flagsIndex is not 0 && flagsIndex % 8 is 0)
                                    builder.AppendIndentedLine("flag = new byte();");

                                builder.AppendIndentedLine("flag = BooleanByteWrapper.SetFlag(flag, {0}, {1});", Convert.ToUInt32(property.WriteMethod), property.Name);

                                flags--;
                                flagsIndex++;

                                if (flags is 0 || flagsIndex % 8 is 0)
                                    builder.AppendIndentedLine("writer.WriteUInt8(flag);");
                                continue;

                            default:
                                if (property.Type is not "byte[]")
                                    throw new Exception("Unsupported property type.");

                                builder.AppendIndentedLine("writer.WriteInt16((short){0}.Length);", property.Name);
                                builder.AppendIndentedLine("writer.WriteSpan({0});", property.Name);
                                continue;
                        }
                        break;

                    case PropertyKind.Object:
                        switch (property.MethodKind)
                        {
                            case MethodKind.SerializeOrDeserialize:
                                break;

                            case MethodKind.ProtocolTypeManager:
                                builder.AppendIndentedLine("writer.WriteUInt16({0}.ProtocolId);", property.Name);
                                break;

                            default:
                                throw new Exception("Unsupported property method.");
                        }

                        builder.AppendIndentedLine("{0}.Serialize(writer);", property.Name);
                        break;

                    case PropertyKind.Vector:
                        RenderSerializeVector(builder, property);
                        break;
                }
            }
        }
    }

    /// <summary>
    /// Renders the deserialization logic for the properties of the specified class symbol.
    /// </summary>
    /// <param name="builder">The <see cref="IndentedStringBuilder"/> used to build the output.</param>
    /// <param name="symbol">The class symbol whose properties are to be deserialized.</param>
    private void RenderDeserializeMethod(IndentedStringBuilder builder, ClassSymbol symbol)
    {
        if (symbol.Properties.Count is 0)
            return;

        builder.AppendIndentedLine("public override void Deserialize(BigEndianReader reader)");

        using (builder.CreateScope())
        {
            if (HaveCustomParent(symbol))
                builder.AppendIndentedLine("base.Deserialize(reader);");

            var flags = symbol.Properties.Values.Count(static x => x.MethodKind is MethodKind.BooleanByteWrapper);
            var flagsIndex = 0;

            if (flags > 0)
                builder.AppendIndentedLine("var flag = reader.ReadUInt8();");

            foreach (var property in symbol.Properties.Values)
            {
                switch (property.PropertyKind)
                {
                    case PropertyKind.Primitive:
                        switch (property.MethodKind)
                        {
                            case MethodKind.Primitive:
                                Debug.Assert(property.ReadMethod is not null);
                                builder.AppendIndentedLine("{0} = reader.{1}();", property.Name, property.ReadMethod);
                                break;

                            case MethodKind.BooleanByteWrapper:
                                Debug.Assert(property.ReadMethod is not null);
                                builder.AppendIndentedLine("{0} = BooleanByteWrapper.GetFlag(flag, {1});", property.Name, Convert.ToUInt32(property.ReadMethod));

                                flags--;
                                flagsIndex++;

                                if (flagsIndex is not 0 && flagsIndex % 8 is 0)
                                    builder.AppendIndentedLine("flag = reader.ReadUInt8();");
                                continue;

                            default:
                                if (property.Type is not "byte[]")
                                    throw new Exception("Unsupported property type.");

                                builder.AppendIndentedLine("reader.ReadSpan(reader.ReadInt16()).ToArray();");
                                continue;
                        }
                        break;

                    case PropertyKind.Object:
                        switch (property.MethodKind)
                        {
                            case MethodKind.SerializeOrDeserialize:
                                Debug.Assert(property.ObjectType is not null);
                                builder.AppendIndentedLine("{0} = {1}.Empty;", property.Name, property.ObjectType);
                                break;

                            case MethodKind.ProtocolTypeManager:
                                Debug.Assert(property.ObjectType is not null);
                                builder.AppendIndentedLine("{0} = Types.TypeFactory.CreateType<{1}>(reader.ReadUInt16());", property.Name, property.ObjectType);
                                break;

                            default:
                                throw new Exception("Unsupported property method.");
                        }

                        builder.AppendIndentedLine("{0}.Deserialize(reader);", property.Name);
                        break;

                    case PropertyKind.Vector:
                        RenderDeserializeVector(builder, symbol, property);
                        break;
                }
            }
        }
    }

    /// <summary>
    /// Serializes a vector property into the provided writer.
    /// </summary>
    /// <param name="builder">The <see cref="IndentedStringBuilder"/> used to build the output.</param>
    /// <param name="property">The vector property to serialize.</param>
    private static void RenderSerializeVector(IndentedStringBuilder builder, PropertySymbol property)
    {
        builder.AppendIndentedLine("var {0}Before = writer.Position;", property.Name.Uncapitalize());
        builder.AppendIndentedLine("var {0}Count = 0;", property.Name.Uncapitalize());

        if (!property.VectorLength.HasValue)
        {
            if (!string.IsNullOrEmpty(property.VectorFieldWrite))
                builder.AppendIndentedLine("writer.{0}(0);", property.VectorFieldWrite);
            else
                builder.AppendIndentedLine("writer.WriteInt16(0);");
        }

        builder.AppendIndentedLine("foreach (var item in {0})", property.Name);

        using (builder.CreateScope())
        {
            switch (property.MethodKind)
            {
                case MethodKind.ProtocolTypeManager:
                    builder.AppendIndentedLine("writer.WriteUInt16(item.ProtocolId);");
                    builder.AppendIndentedLine("item.Serialize(writer);");
                    break;

                case MethodKind.VectorPrimitive:
                    Debug.Assert(property.WriteMethod is not null);
                    builder.AppendIndentedLine("writer.{0}(item);", property.WriteMethod);
                    break;

                default:
                    builder.AppendIndentedLine("item.Serialize(writer);");
                    break;
            }

            builder.AppendIndentedLine("{0}Count++;", property.Name.Uncapitalize());
        }

        builder.AppendIndentedLine("var {0}After = writer.Position;", property.Name.Uncapitalize());
        builder.AppendIndentedLine("writer.Seek(SeekOrigin.Begin, {0}Before);", property.Name.Uncapitalize());

        if (!property.VectorLength.HasValue)
        {
            if (!string.IsNullOrEmpty(property.VectorFieldWrite))
                builder.AppendIndentedLine("writer.{0}(({1}){2}Count);", property.VectorFieldWrite, property.Type, property.Name.Uncapitalize());
            else
                builder.AppendIndentedLine("writer.WriteInt16((short){0}Count);", property.Name.Uncapitalize());
        }

        builder.AppendIndentedLine("writer.Seek(SeekOrigin.Begin, {0}After);", property.Name.Uncapitalize());
    }

    /// <summary>
    /// Deserializes a vector property from the provided reader and assigns it to the specified property.
    /// </summary>
    /// <param name="builder">The <see cref="IndentedStringBuilder"/> used to build the output.</param>
    /// <param name="symbol">The class symbol containing the vector property.</param>
    /// <param name="property">The vector property to deserialize.</param>
    private void RenderDeserializeVector(IndentedStringBuilder builder, ClassSymbol symbol, PropertySymbol property)
    {
        var vectorLengthVar = $"{property.Name.Uncapitalize()}Count";

        if (!property.VectorLength.HasValue)
        {
            if (!string.IsNullOrEmpty(property.VectorFieldRead))
                builder.AppendIndentedLine("var {0} = reader.{1}();", vectorLengthVar, property.VectorFieldRead);
            else
                builder.AppendIndentedLine("var {0} = reader.ReadInt16();", vectorLengthVar);
        }
        else
            vectorLengthVar = property.VectorLength.ToString()!;

        Debug.Assert(property.ObjectType is not null);

        var objectType = property.ObjectType;

        if (GetRecursiveProperties(symbol).Any(x => x.Name.Equals(objectType, StringComparison.OrdinalIgnoreCase)))
        {
            if (_symbolStorage.TryGetSymbol(objectType, out var objectSymbol))
                objectType = string.Concat(objectSymbol.Metadata.Namespace, objectSymbol.Metadata.Name);
        }

        builder.AppendIndentedLine("var {0} = new {1}[{2}];", property.Name.Uncapitalize(), property.ObjectType, vectorLengthVar);
        builder.AppendIndentedLine("for (var i = 0; i < {0}; i++)", vectorLengthVar);

        using (builder.CreateScope())
        {
            switch (property.MethodKind)
            {
                case MethodKind.ProtocolTypeManager:
                    builder.AppendIndentedLine("var entry = Types.TypeFactory.CreateType<{0}>(reader.ReadUInt16());", property.ObjectType);
                    break;

                case null:
                case MethodKind.SerializeOrDeserialize:
                    builder.AppendIndentedLine("var entry = {0}.Empty;", objectType);
                    break;
            }

            switch (property.MethodKind)
            {
                case MethodKind.VectorPrimitive:
                    Debug.Assert(property.ReadMethod is not null);
                    builder.AppendIndentedLine("{0}[i] = reader.{1}();", property.Name.Uncapitalize(), property.ReadMethod);
                    break;

                default:
                    builder.AppendIndentedLine("entry.Deserialize(reader);");
                    builder.AppendIndentedLine("{0}[i] = entry;", property.Name.Uncapitalize());
                    break;
            }
        }

        builder.AppendIndentedLine("{0} = {1};", property.Name, property.Name.Uncapitalize());
    }

    /// <summary>
    /// Determines whether the specified class symbol has a custom parent class.
    /// </summary>
    /// <param name="symbol">The class symbol to check.</param>
    /// <returns>
    /// <c>true</c> if the class symbol has a custom parent class (i.e., not "DofusMessage" or "DofusType");
    /// otherwise, <c>false</c>.
    /// </returns>
    private static bool HaveCustomParent(ClassSymbol symbol)
    {
        return symbol.Metadata.ParentName is not "DofusMessage" && symbol.Metadata.ParentName is not "DofusType";
    }
}
