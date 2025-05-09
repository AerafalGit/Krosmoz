// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Serialization.D2O;
using Krosmoz.Servers.GameServer.Database.Repositories.Datacenter;
using Krosmoz.Tools.Protocol.Extensions;
using Krosmoz.Tools.Protocol.Models;
using Krosmoz.Tools.Protocol.Renderers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Krosmoz.Tools.Protocol.Generators;

/// <summary>
/// Represents a service that generates datacenter symbols from D2O files and writes them to disk.
/// </summary>
public sealed class DatacenterGenerator : IHostedService
{
    private readonly IDatacenterRepository _datacenterRepository;
    private readonly IRenderer<DatacenterSymbol> _datacenterRenderer;
    private readonly IRenderer<DatacenterSymbol> _datacenterObjectFactoryRenderer;

    /// <summary>
    /// Initializes a new instance of the <see cref="DatacenterGenerator"/> class.
    /// </summary>
    /// <param name="datacenterRepository">The repository for accessing datacenter-related data.</param>
    /// <param name="datacenterRenderer">The renderer used to generate source code for datacenter symbols.</param>
    /// <param name="datacenterObjectFactoryRenderer">The renderer used to generate source code for the datacenter object factory.</param>
    public DatacenterGenerator(
        IDatacenterRepository datacenterRepository,
        [FromKeyedServices(nameof(DatacenterRenderer))] IRenderer<DatacenterSymbol> datacenterRenderer,
        [FromKeyedServices(nameof(DatacenterObjectFactoryRenderer))] IRenderer<DatacenterSymbol> datacenterObjectFactoryRenderer)
    {
        _datacenterRepository = datacenterRepository;
        _datacenterRenderer = datacenterRenderer;
        _datacenterObjectFactoryRenderer = datacenterObjectFactoryRenderer;
    }

    /// <summary>
    /// Starts the datacenter generation process asynchronously.
    /// </summary>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public Task StartAsync(CancellationToken cancellationToken)
    {
        return Task.Run(() =>
        {
            var datacenterModuleFilePaths = Directory.EnumerateFiles(_datacenterRepository.DofusCommonPath, "*.d2o").ToArray();

            var d2OFile = new D2OFile(null!);

            foreach (var filePath in datacenterModuleFilePaths)
                d2OFile.RegisterDefinition(filePath);

            var classes = d2OFile.GetClasses();

            foreach (var d2OClassesByModule in classes)
            {
                foreach (var d2OClass in d2OClassesByModule.Value)
                {
                    var datacenterSymbol = new DatacenterSymbol { D2OClasses = classes, ModuleName = d2OClassesByModule.Key, D2OClass = d2OClass.Value };

                    var datacenterSymbolSource = _datacenterRenderer.Render(datacenterSymbol);
                    var datacenterSymbolFilePath = d2OClass.Value.Namespace.NamespaceToPath();

                    if (!Directory.Exists(datacenterSymbolFilePath))
                        Directory.CreateDirectory(datacenterSymbolFilePath);

                    var datacenterSymbolFileName = $"{d2OClass.Value.Name}.cs";
                    var datacenterSymbolFileFullPath = Path.Combine(datacenterSymbolFilePath, datacenterSymbolFileName);

                    File.WriteAllText(datacenterSymbolFileFullPath, datacenterSymbolSource);
                }
            }

            var datacenterObjectFactorySymbol = new DatacenterSymbol { D2OClasses = classes, ModuleName = "Datacenter", D2OClass = null! };
            var datacenterObjectFactorySymbolSource = _datacenterObjectFactoryRenderer.Render(datacenterObjectFactorySymbol);
            var datacenterObjectFactorySymbolFilePath = "Krosmoz.Protocol.Datacenter".NamespaceToPath();
            var datacenterObjectFactorySymbolFileFullPath = Path.Combine(datacenterObjectFactorySymbolFilePath, "DatacenterObjectFactory.cs");

            if (!Directory.Exists(datacenterObjectFactorySymbolFilePath))
                Directory.CreateDirectory(datacenterObjectFactorySymbolFilePath);

            File.WriteAllText(datacenterObjectFactorySymbolFileFullPath, datacenterObjectFactorySymbolSource);
        }, cancellationToken);
    }

    /// <summary>
    /// Stops the datacenter generation process asynchronously.
    /// </summary>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A completed task.</returns>
    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
