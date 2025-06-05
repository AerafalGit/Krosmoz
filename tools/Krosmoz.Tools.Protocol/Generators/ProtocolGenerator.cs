// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using System.Diagnostics.CodeAnalysis;
using Krosmoz.Serialization;
using Krosmoz.Tools.Protocol.Converters;
using Krosmoz.Tools.Protocol.Extensions;
using Krosmoz.Tools.Protocol.Models;
using Krosmoz.Tools.Protocol.Parsers;
using Krosmoz.Tools.Protocol.Renderers;
using Krosmoz.Tools.Protocol.Storages.Expressions;
using Krosmoz.Tools.Protocol.Storages.Symbols;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Krosmoz.Tools.Protocol.Generators;

/// <summary>
/// Represents a background service responsible for generating protocol-related files.
/// </summary>
public sealed class ProtocolGenerator : BackgroundService
{
    private readonly ILogger<ProtocolGenerator> _logger;
    private readonly ISymbolStorage _symbolStorage;
    private readonly IParser<EnumSymbol> _enumParser;
    private readonly IConverter<EnumSymbol> _enumConverter;
    private readonly IRenderer<EnumSymbol> _enumRenderer;
    private readonly IParser<ClassSymbol> _classParser;
    private readonly IConverter<ClassSymbol> _classConverter;
    private readonly IRenderer<ClassSymbol> _classRenderer;
    private readonly IRenderer<ClassSymbol[]> _typeFactoryRenderer;
    private readonly IRenderer<ClassSymbol[]> _messageFactoryRenderer;

    /// <summary>
    /// Initializes a new instance of the <see cref="ProtocolGenerator"/> class.
    /// </summary>
    /// <param name="logger">The logger used for logging messages.</param>
    /// <param name="symbolStorage">The storage for managing protocol symbols.</param>
    /// <param name="enumParser">The parser for processing enum symbols.</param>
    /// <param name="enumConverter">The converter for modifying enum symbols.</param>
    /// <param name="enumRenderer">The renderer for generating enum symbol source code.</param>
    /// <param name="classParser">The parser for processing class symbols.</param>
    /// <param name="classConverter">The converter for modifying class symbols.</param>
    /// <param name="classRenderer">The renderer for generating class symbol source code.</param>
    /// <param name="typeFactoryRenderer">The renderer for generating the type factory source code.</param>
    /// <param name="messageFactoryRenderer">The renderer for generating the message factory source code.</param>
    public ProtocolGenerator(
        ILogger<ProtocolGenerator> logger,
        ISymbolStorage symbolStorage,
        IParser<EnumSymbol> enumParser,
        IConverter<EnumSymbol> enumConverter,
        IRenderer<EnumSymbol> enumRenderer,
        IParser<ClassSymbol> classParser,
        IConverter<ClassSymbol> classConverter,
        IRenderer<ClassSymbol> classRenderer,
        [FromKeyedServices(nameof(TypeFactoryRenderer))] IRenderer<ClassSymbol[]> typeFactoryRenderer,
        [FromKeyedServices(nameof(MessageFactoryRenderer))] IRenderer<ClassSymbol[]> messageFactoryRenderer)
    {
        _logger = logger;
        _symbolStorage = symbolStorage;
        _enumParser = enumParser;
        _enumConverter = enumConverter;
        _enumRenderer = enumRenderer;
        _classParser = classParser;
        _classConverter = classConverter;
        _classRenderer = classRenderer;
        _typeFactoryRenderer = typeFactoryRenderer;
        _messageFactoryRenderer = messageFactoryRenderer;
    }

    /// <summary>
    /// Executes the background service to generate protocol-related files.
    /// </summary>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    protected override Task ExecuteAsync(CancellationToken cancellationToken)
    {
        return Task.Run(() =>
        {
            _logger.LogInformation("Starting protocol generation");

            var networkDirectoryPath = Path.Combine(PathConstants.Directories.SourcePath, "com", "ankamagames", "dofus", "network");

            if (!Directory.Exists(networkDirectoryPath))
                throw new DirectoryNotFoundException($"The directory {networkDirectoryPath} does not exist.");

            foreach (var filePath in Directory.EnumerateFiles(networkDirectoryPath, "*.as", SearchOption.AllDirectories))
            {
                if (!TryParseSymbolMetadata(CleanSource(File.ReadAllText(filePath)), out var symbolMetadata))
                {
                    _logger.LogWarning("Ignoring file {FileName} because it does not contain a valid class declaration", Path.GetFileNameWithoutExtension(filePath));
                    continue;
                }

                switch (symbolMetadata.Kind)
                {
                    case SymbolKind.Enum:
                        var enumSymbol = _enumParser.Parse(symbolMetadata);
                        _enumConverter.Convert(enumSymbol);
                        var enumSource = _enumRenderer.Render(enumSymbol);
                        var enumDirectoryPath = "Krosmoz.Protocol.Enums".NamespaceToPath();
                        var enumFilePath = Path.Combine(enumDirectoryPath, string.Concat(enumSymbol.Metadata.Name, '.', "cs"));

                        if (!Directory.Exists(enumDirectoryPath))
                            Directory.CreateDirectory(enumDirectoryPath);

                        File.WriteAllText(enumFilePath, enumSource);
                        break;

                    case SymbolKind.Message:
                    case SymbolKind.Type:
                        var classSymbol = _classParser.Parse(symbolMetadata);
                        _classConverter.Convert(classSymbol);
                        _symbolStorage.AddSymbol(classSymbol);
                        break;
                }
            }

            foreach (var classSymbol in _symbolStorage.GetSymbols())
            {
                var classSource = _classRenderer.Render(classSymbol);
                var classDirectoryPath = classSymbol.Metadata.Namespace.NamespaceToPath();
                var classFilePath = Path.Combine(classDirectoryPath, string.Concat(classSymbol.Metadata.Name, '.', "cs"));

                if (!Directory.Exists(classDirectoryPath))
                    Directory.CreateDirectory(classDirectoryPath);

                File.WriteAllText(classFilePath, classSource);
            }

            var messageSymbols = _symbolStorage
                .GetSymbols()
                .Where(static symbol => symbol.Metadata.Kind is SymbolKind.Message)
                .ToArray();

            var typeSymbols = _symbolStorage
                .GetSymbols()
                .Where(static symbol => symbol.Metadata.Kind is SymbolKind.Type)
                .ToArray();

            var messageFactorySource = _messageFactoryRenderer.Render(messageSymbols);
            var typeFactorySource = _typeFactoryRenderer.Render(typeSymbols);

            var messageFactoryDirectoryPath = "Krosmoz.Protocol.Messages".NamespaceToPath();
            var typeFactoryDirectoryPath = "Krosmoz.Protocol.Types".NamespaceToPath();

            var messageFactoryFilePath = Path.Combine(messageFactoryDirectoryPath, "MessageFactory.cs");
            var typeFactoryFilePath = Path.Combine(typeFactoryDirectoryPath, "TypeFactory.cs");

            if (!Directory.Exists(messageFactoryDirectoryPath))
                Directory.CreateDirectory(messageFactoryDirectoryPath);

            if (!Directory.Exists(typeFactoryDirectoryPath))
                Directory.CreateDirectory(typeFactoryDirectoryPath);

            File.WriteAllText(messageFactoryFilePath, messageFactorySource);
            File.WriteAllText(typeFactoryFilePath, typeFactorySource);

            _logger.LogInformation("Protocol generation completed successfully");
        }, cancellationToken);
    }

    /// <summary>
    /// Attempts to parse symbol metadata from the given source code.
    /// </summary>
    /// <param name="source">The source code to parse.</param>
    /// <param name="metadata">When this method returns, contains the parsed symbol metadata if successful; otherwise, <c>null</c>.</param>
    /// <returns><c>true</c> if the metadata was successfully parsed; otherwise, <c>false</c>.</returns>
    private static bool TryParseSymbolMetadata(string source, [NotNullWhen(true)] out SymbolMetadata? metadata)
    {
        metadata = null;

        var match = RegexStorage.ClassDeclaration().Match(source);

        if (!match.Groups.TryGetValue("name", out var nameGroup))
            return false;

        if (!match.Groups.TryGetValue("parent", out var parentGroup))
            return false;

        if (!match.Groups.TryGetValue("interface", out var interfaceGroup))
            return false;

        match = RegexStorage.NamespaceDeclaration().Match(source);

        if (!match.Groups.TryGetValue("name", out var namespaceGroup))
            return false;

        if (!TryParseSymbolKind(nameGroup.Value, interfaceGroup.Value, out var kind))
        {
            if (source.Contains("public enum"))
                kind = SymbolKind.Enum;
            else
                return false;
        }

        var parent = parentGroup.Value;
        var name = nameGroup.Value;
        var @namespace = namespaceGroup.Value.Replace("com.ankamagames.dofus.network", string.Empty);

        if (parent is "implements")
            parent = "NetworkType";

        metadata = new SymbolMetadata
        {
            Name = name,
            Namespace = @namespace,
            ParentName = parent,
            Kind = kind,
            Source = source
        };
        return true;
    }

    /// <summary>
    /// Determines the kind of symbol based on the class name and interface name.
    /// </summary>
    /// <param name="className">The name of the class.</param>
    /// <param name="interfaceName">The name of the interface.</param>
    /// <param name="kind">When this method returns, contains the determined symbol kind if successful.</param>
    /// <returns><c>true</c> if the symbol kind was successfully determined; otherwise, <c>false</c>.</returns>
    private static bool TryParseSymbolKind(string className, string interfaceName, out SymbolKind kind)
    {
        switch (className, interfaceName)
        {
            case var (a, _) when a.EndsWith("Enum"):
                kind = SymbolKind.Enum;
                return true;

            case (_, "INetworkMessage"):
                kind = SymbolKind.Message;
                return true;

            case (_, "INetworkType"):
                kind = SymbolKind.Type;
                return true;

            default:
                kind = default;
                return false;
        }
    }

    /// <summary>
    /// Cleans the source code by removing unnecessary lines and blocks.
    /// </summary>
    /// <param name="src">The source code to clean.</param>
    /// <returns>The cleaned source code.</returns>
    private static string CleanSource(string src)
    {
        var source = src.Split("\n");

        for (var i = 0; i < source.Length; i++)
        {
            var line = source[i];

            if (!line.Contains("if("))
                continue;

            source[i] = string.Empty;

            var openBarakCount = 0;

            for (var subIndex = i; subIndex < source.Length; subIndex++)
            {
                if (source[subIndex].Trim() is "{")
                {
                    source[subIndex] = string.Empty;
                    openBarakCount++;
                }

                if (source[subIndex].Trim() is "}")
                {
                    source[subIndex] = string.Empty;
                    openBarakCount--;

                    if (openBarakCount <= 0)
                        break;
                }

                if (source[subIndex].Trim() is "continue;")
                    source[subIndex] = string.Empty;

                if (source[subIndex].Trim() is "return;")
                    source[subIndex] = string.Empty;

                if (RegexStorage.ThrowError().IsMatch(source[subIndex]))
                    source[subIndex] = string.Empty;
            }
        }

        return source
            .Where(static line => line.Trim() != string.Empty)
            .Aggregate(static (current, line) => current + (line + (char)10));
    }
}
