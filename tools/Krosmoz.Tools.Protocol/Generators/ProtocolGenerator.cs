// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using System.Diagnostics.CodeAnalysis;
using Krosmoz.Servers.GameServer.Database.Repositories.Datacenter;
using Krosmoz.Tools.Protocol.Converters;
using Krosmoz.Tools.Protocol.Extensions;
using Krosmoz.Tools.Protocol.Models;
using Krosmoz.Tools.Protocol.Parsers;
using Krosmoz.Tools.Protocol.Renderers;
using Krosmoz.Tools.Protocol.Storages.Expressions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Krosmoz.Tools.Protocol.Generators;

/// <summary>
/// Represents a hosted service responsible for generating protocol-related data.
/// </summary>
public sealed class ProtocolGenerator : IHostedService
{
    private readonly IDatacenterRepository _datacenterRepository;
    private readonly IParser<EnumSymbol> _enumParser;
    private readonly IConverter<EnumSymbol> _enumConverter;
    private readonly IRenderer<EnumSymbol> _enumRenderer;
    private readonly IParser<ClassSymbol> _classParser;
    private readonly IConverter<ClassSymbol> _classConverter;
    private readonly ILogger<ProtocolGenerator> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="ProtocolGenerator"/> class.
    /// </summary>
    /// <param name="datacenterRepository">The repository for accessing datacenter information.</param>
    /// <param name="enumParser">The parser for parsing enumeration symbols.</param>
    /// <param name="enumConverter">The converter for converting enumeration symbols.</param>
    /// <param name="enumRenderer">The renderer for rendering enumeration symbols.</param>
    /// <param name="classParser">The parser for parsing class symbols.</param>
    /// <param name="classConverter">The converter for converting class symbols.</param>
    /// <param name="logger">The logger for logging information and warnings.</param>
    public ProtocolGenerator(
        IDatacenterRepository datacenterRepository,
        IParser<EnumSymbol> enumParser,
        IConverter<EnumSymbol> enumConverter,
        IRenderer<EnumSymbol> enumRenderer,
        IParser<ClassSymbol> classParser,
        IConverter<ClassSymbol> classConverter,
        ILogger<ProtocolGenerator> logger)
    {
        _datacenterRepository = datacenterRepository;
        _enumParser = enumParser;
        _enumConverter = enumConverter;
        _enumRenderer = enumRenderer;
        _classParser = classParser;
        _classConverter = classConverter;
        _logger = logger;
    }

    /// <summary>
    /// Starts the protocol generation process asynchronously.
    /// </summary>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public Task StartAsync(CancellationToken cancellationToken)
    {
        return Task.Run(() =>
        {
            var networkDirectoryPath = Path.Combine(_datacenterRepository.DofusPath, "fla", "com", "ankamagames", "dofus", "network");

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

                        if (!Directory.Exists(enumDirectoryPath))
                            Directory.CreateDirectory(enumDirectoryPath);

                        File.WriteAllText(Path.Combine(enumDirectoryPath, string.Concat(enumSymbol.Metadata.Name, '.', "cs")), enumSource);
                        break;

                    case SymbolKind.Message:
                    case SymbolKind.Type:
                        var classSymbol = _classParser.Parse(symbolMetadata);
                        _classConverter.Convert(classSymbol);
                        break;
                }
            }
        }, cancellationToken);
    }

    /// <summary>
    /// Stops the protocol generation process asynchronously.
    /// </summary>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A completed task.</returns>
    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
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
            return false;

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
