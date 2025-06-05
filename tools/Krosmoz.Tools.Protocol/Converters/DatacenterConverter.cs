// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Core.Extensions;
using Krosmoz.Tools.Protocol.Models;

namespace Krosmoz.Tools.Protocol.Converters;

/// <summary>
/// Provides functionality to convert a <see cref="DatacenterSymbol"/>.
/// </summary>
public sealed class DatacenterConverter : IConverter<DatacenterSymbol>
{
    /// <summary>
    /// Converts the specified <see cref="DatacenterSymbol"/> by updating its namespace and field names
    /// to follow the required conventions.
    /// </summary>
    /// <param name="symbol">The <see cref="DatacenterSymbol"/> to be converted.</param>
    public void Convert(DatacenterSymbol symbol)
    {
        symbol.D2OClass.Namespace = symbol.D2OClass.Namespace.Contains("datacenter")
            ? string.Concat("Krosmoz.Protocol", '.', string.Join('.', symbol.D2OClass.Namespace.Replace("com.ankamagames.dofus.", string.Empty).Split('.').Select(static x => x.Capitalize())))
            : string.Concat("Krosmoz.Protocol.Datacenter", '.', string.Join('.', symbol.D2OClass.Namespace.Replace("com.ankamagames.dofus.", string.Empty).Split('.').Select(static x => x.Capitalize())));

        symbol.D2OClass.Namespace = symbol.D2OClass.Namespace
            .Replace("Com.Ankamagames.Tiphon.Types", "Tiphon")
            .Replace("Flash.Geom", "Geom")
            .Replace("Externalnotifications", "ExternalNotifications");

        foreach (var d2OField in symbol.D2OClass.Fields)
            d2OField.Name = d2OField.Name.Capitalize();
    }
}
