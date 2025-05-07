// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

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
}
