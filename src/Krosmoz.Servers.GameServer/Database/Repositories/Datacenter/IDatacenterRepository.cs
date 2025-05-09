// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Serialization.D2O.Abstractions;
using Krosmoz.Serialization.I18N;

namespace Krosmoz.Servers.GameServer.Database.Repositories.Datacenter;

/// <summary>
/// Represents a repository interface for accessing various paths related to the Dofus datacenter.
/// </summary>
public interface IDatacenterRepository
{
    /// <summary>
    /// Gets the path to the Dofus game files.
    /// </summary>
    string DofusPath { get; }

    /// <summary>
    /// Gets the path to the Dofus tiles data.
    /// </summary>
    string DofusTilesPath { get; }

    /// <summary>
    /// Gets the path to the Dofus maps data.
    /// </summary>
    string DofusMapsPath { get; }

    /// <summary>
    /// Gets the path to the Dofus common data.
    /// </summary>
    string DofusCommonPath { get; }

    /// <summary>
    /// Gets the path to the Dofus internationalization (I18N) data.
    /// </summary>
    string DofusI18NPath { get; }

    /// <summary>
    /// Retrieves the internationalization (I18N) file containing localized data.
    /// </summary>
    /// <returns>An instance of <see cref="I18NFile"/> representing the I18N data.</returns>
    I18NFile GetI18N();

    /// <summary>
    /// Retrieves a list of objects of the specified type from the datacenter.
    /// </summary>
    /// <typeparam name="T">The type of objects to retrieve, which must implement <see cref="IDatacenterObject"/>.</typeparam>
    /// <param name="clearCache">
    /// A boolean value indicating whether to clear the cache before retrieving the objects.
    /// If <c>true</c>, the cache will be cleared.
    /// </param>
    /// <returns>A list of objects of type <typeparamref name="T"/>.</returns>
    IList<T> GetObjects<T>(bool clearCache = false)
        where T : class, IDatacenterObject;
}
