// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Serialization.D2O;
using Krosmoz.Tools.Protocol.Converters;
using Krosmoz.Tools.Protocol.Extensions;
using Krosmoz.Tools.Protocol.Models;
using Krosmoz.Tools.Protocol.Renderers;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Krosmoz.Tools.Protocol.Generators;

/// <summary>
/// Represents a background service responsible for generating datacenter-related files.
/// </summary>
public sealed class DatacenterGenerator : BackgroundService
{
    private readonly D2OFile _d2OFile;
    private readonly IConverter<DatacenterSymbol> _datacenterConverter;
    private readonly IRenderer<DatacenterSymbol> _datacenterRenderer;
    private readonly IRenderer<Dictionary<string, Dictionary<int, D2OClass>>> _datacenterObjectFactoryRenderer;
    private readonly ILogger<DatacenterGenerator> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="DatacenterGenerator"/> class.
    /// </summary>
    /// <param name="d2OFile">The D2O file repository used to load and manage D2O data.</param>
    /// <param name="datacenterConverter">The converter for modifying datacenter symbols.</param>
    /// <param name="datacenterRenderer">The renderer for generating datacenter symbol source code.</param>
    /// <param name="datacenterObjectFactoryRenderer">The renderer for generating the datacenter object factory source code.</param>
    /// <param name="logger">The logger for logging messages.</param>
    public DatacenterGenerator(
        D2OFile d2OFile,
        IConverter<DatacenterSymbol> datacenterConverter,
        IRenderer<DatacenterSymbol> datacenterRenderer,
        IRenderer<Dictionary<string, Dictionary<int, D2OClass>>> datacenterObjectFactoryRenderer,
        ILogger<DatacenterGenerator> logger)
    {
        _d2OFile = d2OFile;
        _datacenterConverter = datacenterConverter;
        _datacenterRenderer = datacenterRenderer;
        _datacenterObjectFactoryRenderer = datacenterObjectFactoryRenderer;
        _logger = logger;
    }

    /// <summary>
    /// Executes the background service to generate datacenter-related files.
    /// </summary>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    protected override Task ExecuteAsync(CancellationToken cancellationToken)
    {
        return Task.Run(() =>
        {
            _logger.LogInformation("Starting datacenter generation");

            var classes = _d2OFile.GetClasses();

            foreach (var (moduleName, classesById) in classes)
            {
                foreach (var d2OClass in classesById.Values)
                {
                    var symbol = new DatacenterSymbol { ModuleName = moduleName, D2OClasses = classes, D2OClass = d2OClass };

                    _datacenterConverter.Convert(symbol);

                    var symbolSource = _datacenterRenderer.Render(symbol);
                    var symbolDirectoryPath = d2OClass.Namespace.NamespaceToPath();
                    var symbolFilePath = Path.Combine(symbolDirectoryPath, $"{d2OClass.Name}.cs");

                    if (!Directory.Exists(symbolDirectoryPath))
                        Directory.CreateDirectory(symbolDirectoryPath);

                    File.WriteAllText(symbolFilePath, symbolSource);
                }
            }

            var datacenterObjectFactorySource = _datacenterObjectFactoryRenderer.Render(classes);
            var datacenterObjectFactoryDirectoryPath = "Krosmoz.Protocol.Datacenter".NamespaceToPath();
            var datacenterObjectFactoryFilePath = Path.Combine(datacenterObjectFactoryDirectoryPath, "DatacenterObjectFactory.cs");

            if (!Directory.Exists(datacenterObjectFactoryDirectoryPath))
                Directory.CreateDirectory(datacenterObjectFactoryDirectoryPath);

            File.WriteAllText(datacenterObjectFactoryFilePath, datacenterObjectFactorySource);

            _logger.LogInformation("Datacenter generation completed successfully");
        }, cancellationToken);
    }
}
