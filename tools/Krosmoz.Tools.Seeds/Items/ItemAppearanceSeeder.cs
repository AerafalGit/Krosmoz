// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using System.Net.Http.Json;
using System.Text.Json.Serialization;
using Krosmoz.Protocol.Datacenter.Effects.Instances;
using Krosmoz.Protocol.Datacenter.Items;
using Krosmoz.Protocol.Enums.Custom;
using Krosmoz.Servers.AuthServer.Database;
using Krosmoz.Servers.GameServer.Database;
using Krosmoz.Servers.GameServer.Database.Models.Items;
using Krosmoz.Servers.GameServer.Services.Datacenter;
using Krosmoz.Tools.Seeds.Base;
using Microsoft.EntityFrameworkCore;

namespace Krosmoz.Tools.Seeds.Items;

/// <summary>
/// Represents a seeder for populating item appearance data in the database.
/// </summary>
public sealed partial class ItemAppearanceSeeder : BaseSeeder
{
    /// <summary>
    /// Stores the predefined DofusBook item types used for fetching item appearances.
    /// </summary>
    private static readonly string[] s_dofusBookTypes =
    [
        "16", // Chapeau
        "17", // Cape
        "14", // Bouclier
        "22", // Familier
        "24", // Montilier
        "27", // Apparat
        "29", // Costume
        "30", // Hanarchement
        "cam" // Cameleon
    ];

    private readonly IHttpClientFactory _httpClientFactory;

    /// <summary>
    /// Initializes a new instance of the <see cref="ItemAppearanceSeeder"/> class.
    /// </summary>
    /// <param name="datacenterService">The service for accessing datacenter data.</param>
    /// <param name="authDbContext">The authentication database context.</param>
    /// <param name="gameDbContext">The game database context.</param>
    /// <param name="httpClientFactory">The factory for creating HTTP clients.</param>
    public ItemAppearanceSeeder(IDatacenterService datacenterService, AuthDbContext authDbContext, GameDbContext gameDbContext, IHttpClientFactory httpClientFactory)
        : base(datacenterService, authDbContext, gameDbContext)
    {
        _httpClientFactory = httpClientFactory;
    }

    /// <summary>
    /// Seeds the item appearance data into the database asynchronously.
    /// </summary>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    public override async Task SeedAsync(CancellationToken cancellationToken)
    {
        await GameDbContext.ItemAppearances.ExecuteDeleteAsync(cancellationToken);

        var datacenterItems = DatacenterService
            .GetObjects<Item>()
            .ToDictionary(static x => x.Id);

        using var httpClient = _httpClientFactory.CreateClient("DofusBook");

        var itemAppearances = new Dictionary<int, ItemAppearanceRecord>();

        foreach (var dofusBookType in s_dofusBookTypes)
        {
            var container = await httpClient.GetFromJsonAsync<DofusBookItemContainer>(
                $"items/dofus/skinator/category/{dofusBookType}",
                DofusBookItemContainerDeserializerContext.Default.DofusBookItemContainer,
                cancellationToken);

            if (container is null)
                throw new InvalidOperationException($"Failed to fetch DofusBook items for type {dofusBookType}.");

            foreach (var dofusBookItem in container.Items.Where(static x => x is { Official: not null, Swf: not null }))
            {
                if (!datacenterItems.TryGetValue(dofusBookItem.Official!.Value, out var datacenterItem))
                    continue;

                itemAppearances[dofusBookItem.Official!.Value] = new ItemAppearanceRecord
                {
                    Id = datacenterItem.Id,
                    AppearanceId = dofusBookItem.Swf!.Value,
                    CustomLook = UseCustomLook(datacenterItem) ? $"{{{dofusBookItem.Swf!.Value}}}" : null
                };
            }
        }

        GameDbContext.ItemAppearances.AddRange(itemAppearances.Values);

        await GameDbContext.SaveChangesAsync(cancellationToken);
    }

    /// <summary>
    /// Determines whether a custom look should be used for the given item.
    /// </summary>
    /// <param name="item">The item to check.</param>
    /// <returns><c>true</c> if a custom look should be used; otherwise, <c>false</c>.</returns>
    private static bool UseCustomLook(Item item)
    {
        var itemTypeId = (ItemTypeIds)item.TypeId;

        switch (itemTypeId)
        {
            case ItemTypeIds.Familier or ItemTypeIds.Montilier:
                return true;
            case ItemTypeIds.ObjetDapparat:
            {
                var effect = item.PossibleEffects
                    .OfType<EffectInstanceDice>()
                    .FirstOrDefault(static x => x.EffectId is 1179);

                return effect?.Value is (int)ItemTypeIds.Chapeau or
                    (int)ItemTypeIds.Cape or
                    (int)ItemTypeIds.Bouclier or
                    (int)ItemTypeIds.Amulette or
                    (int)ItemTypeIds.Anneau or
                    (int)ItemTypeIds.Ceinture or
                    (int)ItemTypeIds.Bottes or
                    (int)ItemTypeIds.Familier or
                    (int)ItemTypeIds.Montilier;
            }
        }

        return false;
    }

    /// <summary>
    /// Represents a container for DofusBook items.
    /// </summary>
    private sealed class DofusBookItemContainer
    {
        /// <summary>
        /// Gets or sets the collection of DofusBook items.
        /// </summary>
        [JsonPropertyName("data")]
        public required IEnumerable<DofusBookItem> Items { get; set; }
    }

    /// <summary>
    /// Represents a DofusBook item with appearance data.
    /// </summary>
    private sealed class DofusBookItem
    {
        /// <summary>
        /// Gets or sets the official ID of the item.
        /// </summary>
        [JsonPropertyName("official")]
        public int? Official { get; set; }

        /// <summary>
        /// Gets or sets the SWF ID of the item.
        /// </summary>
        [JsonPropertyName("swf")]
        public int? Swf { get; set; }
    }

    /// <summary>
    /// Provides a context for deserializing DofusBook item containers.
    /// </summary>
    [JsonSerializable(typeof(DofusBookItemContainer))]
    private sealed partial class DofusBookItemContainerDeserializerContext : JsonSerializerContext;
}
