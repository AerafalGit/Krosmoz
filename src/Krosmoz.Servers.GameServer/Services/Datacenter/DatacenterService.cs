// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Datacenter;
using Krosmoz.Serialization;
using Krosmoz.Serialization.D2I;
using Krosmoz.Serialization.D2O;
using Krosmoz.Serialization.D2O.Abstractions;
using Krosmoz.Serialization.D2P;
using Krosmoz.Serialization.ELE;
using Microsoft.Extensions.Logging;

namespace Krosmoz.Servers.GameServer.Services.Datacenter;

/// <summary>
/// Provides functionality to manage and access datacenter files and objects.
/// </summary>
public sealed class DatacenterService : IDatacenterService
{
    private readonly ILogger<DatacenterService> _logger;

    private D2IFile? _d2I;
    private D2OFile? _d2O;

    /// <summary>
    /// Initializes a new instance of the <see cref="DatacenterService"/> class.
    /// </summary>
    /// <param name="logger">The logger instance for logging operations.</param>
    public DatacenterService(ILogger<DatacenterService> logger)
    {
        _logger = logger;
    }

    /// <summary>
    /// Retrieves the D2I file, which contains internationalized text data.
    /// </summary>
    /// <returns>The loaded D2I file instance.</returns>
    public D2IFile GetD2I()
    {
        if (_d2I is null)
        {
            _d2I = new D2IFile();
            _d2I.Load();

            _logger.LogDebug("Loaded {Count} objects of type {Type} from source {Source}", _d2I.IndexedTexts.Count, "D2ITexts", "D2IFile");
        }

        return _d2I;
    }

    /// <summary>
    /// Retrieves the D2O file, which contains object data definitions.
    /// </summary>
    /// <returns>The loaded D2O file instance.</returns>
    public D2OFile GetD2O()
    {
        return _d2O ??= new D2OFile(new DatacenterObjectFactory(), GetD2I());
    }

    /// <summary>
    /// Retrieves the D2P file, which contains packed data files.
    /// </summary>
    /// <returns>A new instance of the D2P file.</returns>
    public D2PFile GetD2P()
    {
        return new D2PFile();
    }

    /// <summary>
    /// Retrieves the D2P file containing map data.
    /// </summary>
    /// <returns>A new instance of the D2P file for maps.</returns>
    public D2PFile GetMaps()
    {
        return new D2PFile(Path.Combine(PathConstants.Directories.MapsPath, "maps0.d2p"));
    }

    /// <summary>
    /// Retrieves the D2P file containing tile data.
    /// </summary>
    /// <returns>A new instance of the D2P file for tiles.</returns>
    public D2PFile GetTiles()
    {
        return new D2PFile(Path.Combine(PathConstants.Directories.TilesPath, "gfx0.d2p"));
    }

    /// <summary>
    /// Retrieves the ELE file, which contains graphical element data.
    /// </summary>
    /// <returns>The loaded graphical element file instance.</returns>
    public GraphicalElementFile GetEle()
    {
        return GraphicalElementAdapter.Load();
    }

    /// <summary>
    /// Retrieves a list of objects of the specified type from the datacenter.
    /// </summary>
    /// <typeparam name="T">The type of objects to retrieve.</typeparam>
    /// <param name="clearCache">Indicates whether to clear the cache before retrieving objects.</param>
    /// <returns>A list of objects of the specified type.</returns>
    public IList<T> GetObjects<T>(bool clearCache = false)
        where T : class, IDatacenterObject
    {
        var objects = GetD2O().GetObjects<T>(clearCache);

        _logger.LogInformation("Loaded {Count} objects of type {Type} from {Source}", objects.Count, T.ModuleName, "Datacenter");

        return objects;
    }

    /// <summary>
    /// Sets a list of objects of the specified type in the datacenter.
    /// </summary>
    /// <typeparam name="T">The type of objects to set.</typeparam>
    /// <param name="objects">The list of objects to set.</param>
    public void SetObjects<T>(IList<T> objects)
        where T : class, IDatacenterObject
    {
        GetD2O().Write(objects);

        _logger.LogInformation("{Count} objects of type {Type} written in {Source}", objects.Count, T.ModuleName, "Datacenter");
    }
}
