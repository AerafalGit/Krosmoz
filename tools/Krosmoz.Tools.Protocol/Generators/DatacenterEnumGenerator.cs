// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using System.Diagnostics.CodeAnalysis;
using Krosmoz.Core.Extensions;
using Krosmoz.Core.IO.Text;
using Krosmoz.Protocol.Datacenter.Alignments;
using Krosmoz.Protocol.Datacenter.Almanax;
using Krosmoz.Protocol.Datacenter.Appearance;
using Krosmoz.Protocol.Datacenter.Breeds;
using Krosmoz.Protocol.Datacenter.Challenges;
using Krosmoz.Protocol.Datacenter.Characteristics;
using Krosmoz.Protocol.Datacenter.Communication;
using Krosmoz.Protocol.Datacenter.Guild;
using Krosmoz.Protocol.Datacenter.Interactives;
using Krosmoz.Protocol.Datacenter.Items;
using Krosmoz.Protocol.Datacenter.Jobs;
using Krosmoz.Protocol.Datacenter.Misc;
using Krosmoz.Protocol.Datacenter.Monsters;
using Krosmoz.Protocol.Datacenter.Mounts;
using Krosmoz.Protocol.Datacenter.Npcs;
using Krosmoz.Protocol.Datacenter.Quest;
using Krosmoz.Protocol.Datacenter.Servers;
using Krosmoz.Protocol.Datacenter.Spells;
using Krosmoz.Protocol.Datacenter.World;
using Krosmoz.Serialization.D2O;
using Krosmoz.Serialization.D2O.Abstractions;
using Krosmoz.Tools.Protocol.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Krosmoz.Tools.Protocol.Generators;

/// <summary>
/// Represents a background service responsible for generating enums for datacenter objects.
/// </summary>
public sealed class DatacenterEnumGenerator : BackgroundService
{
    private readonly D2OFile _d2OFile;
    private readonly ILogger<DatacenterEnumGenerator> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="DatacenterEnumGenerator"/> class.
    /// </summary>
    /// <param name="d2OFile">The D2O file containing the datacenter objects to generate enums for.</param>
    /// <param name="logger">The logger used for logging messages.</param>
    public DatacenterEnumGenerator(D2OFile d2OFile, ILogger<DatacenterEnumGenerator> logger)
    {
        _d2OFile = d2OFile;
        _logger = logger;
    }

    /// <summary>
    /// Executes the background service to generate enums for datacenter objects.
    /// </summary>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    protected override Task ExecuteAsync(CancellationToken cancellationToken)
    {
        return Task.Run(() =>
        {
            _logger.LogInformation("Generating datacenter enums");

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
            BuildDatacenterEnum<TaxCollectorFirstname>(customFieldName: nameof(TaxCollectorFirstname.Firstname));
            BuildDatacenterEnum<TaxCollectorName>();

            BuildDatacenterEnum<AchievementCategory>();
            BuildDatacenterEnum<QuestCategory>();

            BuildDatacenterEnum<Server>();
            BuildDatacenterEnum<ServerCommunity>();
            BuildDatacenterEnum<ServerGameType>();
            BuildDatacenterEnum<ServerPopulation>();

            BuildDatacenterEnum<Breed>(customFieldName: nameof(Breed.ShortName));
            BuildDatacenterEnum<BreedRole>();

            BuildDatacenterEnum<Item>();
            BuildDatacenterEnum<ItemSet>();
            BuildDatacenterEnum<ItemType>();

            BuildDatacenterEnum<Job>();
            BuildDatacenterEnum<Recipe>(customFieldName: nameof(Recipe.ResultName), customFieldId: nameof(Recipe.ResultId));

            BuildDatacenterEnum<Challenge>();

            BuildDatacenterEnum<Characteristic>(customFieldName: nameof(Characteristic.Keyword));
            BuildDatacenterEnum<CharacteristicCategory>();

            BuildDatacenterEnum<OptionalFeature>(customFieldName: nameof(OptionalFeature.Keyword));

            BuildDatacenterEnum<Ornament>();
            BuildDatacenterEnum<Title>(customFieldName: nameof(Title.NameMale));
            BuildDatacenterEnum<TitleCategory>();

            BuildDatacenterEnum<Spell>();
            BuildDatacenterEnum<SpellState>();
            BuildDatacenterEnum<SpellType>(customFieldName: nameof(SpellType.ShortName));

            BuildDatacenterEnum<Area>();
            BuildDatacenterEnum<SubArea>();
            BuildDatacenterEnum<SuperArea>();
            BuildDatacenterEnum<Dungeon>();
            BuildDatacenterEnum<MapPosition>();
            BuildDatacenterEnum<Hint>();
            BuildDatacenterEnum<HintCategory>();
            BuildDatacenterEnum<WorldMap>();

            BuildDatacenterEnum<Interactive>();
            BuildDatacenterEnum<Skill>();
            BuildDatacenterEnum<SkillName>();

            BuildDatacenterEnum<RankName>();

            BuildDatacenterEnum<ChatChannel>();

            BuildDatacenterEnum<Emoticon>();

            _logger.LogInformation("Datacenter enums generated successfully");
        }, cancellationToken);
    }

    /// <summary>
    /// Builds an enum for the specified datacenter object type and writes it to a file.
    /// </summary>
    /// <typeparam name="T">The type of the datacenter object.</typeparam>
    /// <param name="customFieldId">An optional custom field name for the ID property.</param>
    /// <param name="customFieldName">An optional custom field name for the name property.</param>
    private void BuildDatacenterEnum<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)] T>(string? customFieldId = null, string? customFieldName = null)
        where T : class, IDatacenterObject
    {
        var objects = _d2OFile.GetObjects<T>();
        var objectType = typeof(T);

        var idProperty = objectType.GetProperty(customFieldId ?? "Id");
        var nameProperty = objectType.GetProperty(customFieldName ?? "Name");

        if (idProperty is null || nameProperty is null)
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

                var name = (string)nameProperty.GetValue(obj)!;

                name = string.Join(string.Empty, name.Split(['_', '-', ' ']).Select<string, string>(static x => x.Capitalize()));

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
                    .Replace("Î", "I")
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
