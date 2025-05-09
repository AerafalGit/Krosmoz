// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Core.Extensions;
using Krosmoz.Core.IO.Text;
using Krosmoz.Protocol.Datacenter.Alignments;
using Krosmoz.Protocol.Datacenter.Almanax;
using Krosmoz.Protocol.Datacenter.Appearance;
using Krosmoz.Protocol.Datacenter.Breeds;
using Krosmoz.Protocol.Datacenter.Challenges;
using Krosmoz.Protocol.Datacenter.Communication;
using Krosmoz.Protocol.Datacenter.Guild;
using Krosmoz.Protocol.Datacenter.Interactives;
using Krosmoz.Protocol.Datacenter.Items;
using Krosmoz.Protocol.Datacenter.Jobs;
using Krosmoz.Protocol.Datacenter.Misc;
using Krosmoz.Protocol.Datacenter.Monsters;
using Krosmoz.Protocol.Datacenter.Mounts;
using Krosmoz.Protocol.Datacenter.Npcs;
using Krosmoz.Protocol.Datacenter.Servers;
using Krosmoz.Protocol.Datacenter.Spells;
using Krosmoz.Protocol.Datacenter.World;
using Krosmoz.Serialization.D2O.Abstractions;
using Krosmoz.Serialization.I18N;
using Krosmoz.Servers.GameServer.Database.Repositories.Datacenter;
using Krosmoz.Tools.Protocol.Extensions;
using Microsoft.Extensions.Hosting;

namespace Krosmoz.Tools.Protocol.Generators;

/// <summary>
/// Generates enums for datacenter objects and writes them to files.
/// Implements the <see cref="IHostedService"/> interface to run as a hosted service.
/// </summary>
public sealed class DatacenterEnumGenerator : IHostedService
{
    private readonly IDatacenterRepository _datacenterRepository;

    /// <summary>
    /// Initializes a new instance of the <see cref="DatacenterEnumGenerator"/> class.
    /// </summary>
    /// <param name="datacenterRepository">The repository used to retrieve datacenter objects and I18N data.</param>
    public DatacenterEnumGenerator(IDatacenterRepository datacenterRepository)
    {
        _datacenterRepository = datacenterRepository;
    }

    /// <summary>
    /// Starts the hosted service and generates enums for various datacenter objects.
    /// </summary>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public Task StartAsync(CancellationToken cancellationToken)
    {
        return Task.Run(() =>
        {
            BuildDatacenterEnum<AlmanaxCalendar>();

            BuildDatacenterEnum<AlignmentGift>();
            BuildDatacenterEnum<AlignmentOrder>();
            BuildDatacenterEnum<AlignmentRank>();
            BuildDatacenterEnum<AlignmentSide>();

            BuildDatacenterEnum<Monster>();
            BuildDatacenterEnum<MonsterRace>();
            BuildDatacenterEnum<MonsterSuperRace>();

            BuildDatacenterEnum<Mount>();
            BuildDatacenterEnum<MountBehavior>();

            BuildDatacenterEnum<Npc>();
            BuildDatacenterEnum<NpcAction>();
            BuildDatacenterEnum<TaxCollectorFirstname>(customFieldName: nameof(TaxCollectorFirstname.FirstnameId));
            BuildDatacenterEnum<TaxCollectorName>();

            BuildDatacenterEnum<Server>();
            BuildDatacenterEnum<ServerCommunity>();
            BuildDatacenterEnum<ServerGameType>();
            BuildDatacenterEnum<ServerPopulation>();

            BuildDatacenterEnum<Breed>(customFieldName: nameof(Breed.ShortNameId));

            BuildDatacenterEnum<Item>();
            BuildDatacenterEnum<ItemSet>();
            BuildDatacenterEnum<ItemType>();

            BuildDatacenterEnum<Job>();

            BuildDatacenterEnum<Challenge>();

            BuildDatacenterEnum<OptionalFeature>(customFieldName: nameof(OptionalFeature.Keyword));

            BuildDatacenterEnum<Ornament>();
            BuildDatacenterEnum<Title>(customFieldName: nameof(Title.NameMaleId));
            BuildDatacenterEnum<TitleCategory>();

            BuildDatacenterEnum<Spell>();
            BuildDatacenterEnum<SpellState>();
            BuildDatacenterEnum<SpellType>(customFieldName: nameof(SpellType.ShortNameId));

            BuildDatacenterEnum<Area>();
            BuildDatacenterEnum<SubArea>();
            BuildDatacenterEnum<SuperArea>();
            BuildDatacenterEnum<Dungeon>();
            BuildDatacenterEnum<Hint>();
            BuildDatacenterEnum<HintCategory>();
            BuildDatacenterEnum<MapPosition>();

            BuildDatacenterEnum<Interactive>();
            BuildDatacenterEnum<Skill>();

            BuildDatacenterEnum<RankName>();

            BuildDatacenterEnum<ChatChannel>();

            BuildDatacenterEnum<Emoticon>();
        }, cancellationToken);
    }

    /// <summary>
    /// Stops the hosted service.
    /// </summary>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A completed task.</returns>
    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    /// <summary>
    /// Builds an enum for the specified datacenter object type and writes it to a file.
    /// </summary>
    /// <typeparam name="T">The type of the datacenter object.</typeparam>
    /// <param name="customFieldId">An optional custom field name for the ID property.</param>
    /// <param name="customFieldName">An optional custom field name for the name property.</param>
    private void BuildDatacenterEnum<T>(string? customFieldId = null, string? customFieldName = null)
        where T : class, IDatacenterObject
    {
        var i18N = _datacenterRepository.GetI18N();
        var objects = _datacenterRepository.GetObjects<T>();
        var objectType = typeof(T);

        var idProperty = objectType.GetProperty(customFieldId ?? "Id");
        var nameIdProperty = objectType.GetProperty(customFieldName ?? "NameId");

        if (idProperty is null || nameIdProperty is null)
            throw new InvalidOperationException($"Properties {customFieldId} or {customFieldName} not found in {objectType.Name}.");

        var builder = new IndentedStringBuilder()
            .AppendLine("// Copyright (c) Krosmoz 2025.")
            .AppendLine("// Krosmoz licenses this file to you under the MIT license.")
            .AppendLine("// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.")
            .AppendLine()
            .AppendLine("namespace Krosmoz.Protocol.Enums.Custom;")
            .AppendLine()
            .AppendLine("public enum {0}Ids", objectType.Name);

        using (builder.CreateScope())
        {
            var entries = new List<(string, int)>();

            foreach (var obj in objects)
            {
                var id = (int)idProperty.GetValue(obj)!;

                var name = nameIdProperty.PropertyType == typeof(string)
                    ? i18N.GetText(Languages.French, (string)nameIdProperty.GetValue(obj)!)
                    : i18N.GetText(Languages.French, (int)nameIdProperty.GetValue(obj)!);

                name = string.Join(string.Empty, name.Split(['_', '-', ' ']).Select(static x => x.Capitalize()));

                if (name.HasAccents())
                    name = name.RemoveAccents();

                name = name
                    .Replace("<u>", string.Empty)
                    .Replace("<br>", string.Empty)
                    .Replace("</u>", string.Empty)
                    .Replace("é", "e")
                    .Replace("É", "E")
                    .Replace("è", "e")
                    .Replace("ç", "c")
                    .Replace("Ç", "C")
                    .Replace(":", string.Empty)
                    .Replace("â", "a")
                    .Replace("=", string.Empty)
                    .Replace("à", "a")
                    .Replace("ê", "e")
                    .Replace("%", string.Empty)
                    .Replace("°", string.Empty)
                    .Replace("\"", string.Empty)
                    .Replace("'", string.Empty)
                    .Replace(" ", string.Empty)
                    .Replace("(", string.Empty)
                    .Replace(")", string.Empty)
                    .Replace("-", string.Empty)
                    .Replace(".", string.Empty)
                    .Replace("&", "ET")
                    .Replace("__", string.Empty)
                    .Replace("[", string.Empty)
                    .Replace("î", "i")
                    .Replace("]", string.Empty)
                    .Replace("/", "Or")
                    .Replace("?", string.Empty)
                    .Replace("+", "Plus")
                    .Replace("«", string.Empty)
                    .Replace("»", string.Empty)
                    .Replace("!", string.Empty)
                    .Replace(",", string.Empty)
                    .Replace("Ï", "I")
                    .Replace(" ", string.Empty)
                    .Replace("²", "2")
                    .Replace("$", "S")
                    .Replace("*", string.Empty)
                    .Replace("Ô", "O")
                    .Replace("Œ", "OE")
                    .Replace(" ", string.Empty)
                    .Replace("▯", string.Empty)
                    .Replace(";", string.Empty)
                    .Replace(" ", string.Empty)
                    .Replace("œ", "oe")
                    .Replace("-", string.Empty)
                    .Replace("ô", "o")
                    .Replace("\\n", string.Empty)
                    .Replace("–", string.Empty);

                if (string.IsNullOrEmpty(name))
                    continue;

                if (char.IsDigit(name[0]))
                    name = string.Concat('E', name);

                if (name.StartsWith("TextNotFound", StringComparison.OrdinalIgnoreCase))
                    continue;

                if (name.StartsWith("NotFound", StringComparison.OrdinalIgnoreCase))
                    continue;

                if (name.StartsWith("UnknownText", StringComparison.OrdinalIgnoreCase))
                    continue;

                entries.Add((name, id));
            }

            foreach (var (name, value) in entries.ToArray())
            {
                var realName = name;

                if (entries.Count(x => x.Item1.Equals(name)) > 1)
                    realName = string.Concat(name, '_', value);

                builder.AppendIndentedLine("{0} = {1},", realName, value);
            }
        }

        var directoryPath = "Krosmoz.Protocol.Enums.Custom".NamespaceToPath();

        if (!Directory.Exists(directoryPath))
            Directory.CreateDirectory(directoryPath);

        var filePath = Path.Combine(directoryPath, $"{objectType.Name}Ids.cs");

        File.WriteAllText(filePath, builder.ToString());
    }
}
