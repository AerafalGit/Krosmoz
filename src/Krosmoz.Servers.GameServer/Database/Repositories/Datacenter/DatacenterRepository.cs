// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Servers.GameServer.Database.Repositories.Datacenter;

/// <inheritdoc />
public sealed class DatacenterRepository : IDatacenterRepository
{
    /// <inheritdoc />
    public string DofusPath { get; }

    /// <inheritdoc />
    public string DofusTilesPath { get; }

    /// <inheritdoc />
    public string DofusMapsPath { get; }

    /// <inheritdoc />
    public string DofusCommonPath { get; }

    /// <inheritdoc />
    public string DofusI18NPath { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="DatacenterRepository"/> class.
    /// Sets up the paths for various Dofus datacenter resources.
    /// </summary>
    public DatacenterRepository()
    {
        DofusPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Krosmoz");
        DofusMapsPath = Path.Combine(DofusPath, "content", "maps");
        DofusTilesPath = Path.Combine(DofusPath, "content", "gfx", "world");
        DofusCommonPath = Path.Combine(DofusPath, "data", "common");
        DofusI18NPath = Path.Combine(DofusPath, "data", "i18n");
    }
}
