// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Serialization.D2I;
using Krosmoz.Serialization.D2O;
using Krosmoz.Serialization.D2O.Abstractions;
using Krosmoz.Serialization.D2P;
using Krosmoz.Serialization.ELE;

namespace Krosmoz.Servers.GameServer.Services.Datacenter;

/// <summary>
/// Defines the contract for a service that provides access to various datacenter files and objects.
/// </summary>
public interface IDatacenterService
{
    /// <summary>
    /// Retrieves the D2I file, which contains internationalized text data.
    /// </summary>
    /// <returns>The D2I file instance.</returns>
    D2IFile GetD2I();

    /// <summary>
    /// Retrieves the D2O file, which contains object data definitions.
    /// </summary>
    /// <returns>The D2O file instance.</returns>
    D2OFile GetD2O();

    /// <summary>
    /// Retrieves the D2P file, which contains packed data files.
    /// </summary>
    /// <returns>The D2P file instance.</returns>
    D2PFile GetD2P();

    /// <summary>
    /// Retrieves the D2P file containing map data.
    /// </summary>
    /// <returns>The D2P file instance for maps.</returns>
    D2PFile GetMaps();

    /// <summary>
    /// Retrieves the D2P file containing tile data.
    /// </summary>
    /// <returns>The D2P file instance for tiles.</returns>
    D2PFile GetTiles();

    /// <summary>
    /// Retrieves the ELE file, which contains graphical element data.
    /// </summary>
    /// <returns>The graphical element file instance.</returns>
    GraphicalElementFile GetEle();

    /// <summary>
    /// Retrieves a list of objects of the specified type from the datacenter.
    /// </summary>
    /// <typeparam name="T">The type of objects to retrieve.</typeparam>
    /// <param name="clearCache">Indicates whether to clear the cache before retrieving objects.</param>
    /// <returns>A list of objects of the specified type.</returns>
    IList<T> GetObjects<T>(bool clearCache = false)
        where T : class, IDatacenterObject;

    /// <summary>
    /// Sets a list of objects of the specified type in the datacenter.
    /// </summary>
    /// <typeparam name="T">The type of objects to set.</typeparam>
    /// <param name="objects">The list of objects to set.</param>
    void SetObjects<T>(IList<T> objects)
        where T : class, IDatacenterObject;
}
