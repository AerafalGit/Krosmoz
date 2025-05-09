// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Datacenter;
using Krosmoz.Serialization.D2O;
using Krosmoz.Serialization.D2O.Abstractions;
using Krosmoz.Serialization.I18N;

namespace Krosmoz.Servers.GameServer.Database.Repositories.Datacenter;

/// <inheritdoc />
public sealed class DatacenterRepository : IDatacenterRepository
{
    private I18NFile? _i18NFile;

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

    /// <inheritdoc />
    public I18NFile GetI18N()
    {
        if (_i18NFile is null)
        {
            _i18NFile = new I18NFile(DofusI18NPath);
            _i18NFile.Load();
        }

        return _i18NFile;
    }

    /// <inheritdoc />
    public IList<T> GetObjects<T>(bool clearCache = false)
        where T : class, IDatacenterObject
    {
        var d2OFile = new D2OFile(new DatacenterObjectFactory());
        d2OFile.RegisterDefinition(Path.Combine(DofusCommonPath, string.Concat(T.ModuleName, ".d2o")));
        return d2OFile.GetObjects<T>(clearCache);
    }
}
