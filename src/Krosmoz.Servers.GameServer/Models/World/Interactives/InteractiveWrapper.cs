// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using System.Globalization;
using Krosmoz.Protocol.Enums;
using Krosmoz.Protocol.Enums.Custom;
using Krosmoz.Protocol.Types.Game.Interactive;
using Krosmoz.Servers.GameServer.Database.Models.Interactives;
using Krosmoz.Servers.GameServer.Models.Actors.Characters;
using Krosmoz.Servers.GameServer.Models.World.Cells;
using Krosmoz.Servers.GameServer.Models.World.Maps;

namespace Krosmoz.Servers.GameServer.Models.World.Interactives;

/// <summary>
/// Represents a wrapper for interactive elements in the game world.
/// </summary>
public sealed class InteractiveWrapper
{
    /// <summary>
    /// Gets the record associated with the interactive element.
    /// </summary>
    public InteractiveRecord Record { get; }

    /// <summary>
    /// Gets the list of actions associated with the interactive element.
    /// </summary>
    public List<InteractiveActionRecord> Actions { get; }

    /// <summary>
    /// Gets the unique identifier of the interactive element.
    /// </summary>
    public int ElementId { get; }

    /// <summary>
    /// Gets the type identifier of the interactive element.
    /// </summary>
    public int ElementTypeId { get; }

    /// <summary>
    /// Gets the skill identifier of the interactive element.
    /// </summary>
    public int ElementSkillId { get; }

    /// <summary>
    /// Gets the cell identifier where the interactive element is located.
    /// </summary>
    public short CellId { get; }

    /// <summary>
    /// Gets the graphical identifier of the interactive element.
    /// </summary>
    public int GfxId { get; }

    /// <summary>
    /// Gets a value indicating whether the interactive element is animated.
    /// </summary>
    public bool Animated { get; }

    /// <summary>
    /// Gets a value indicating whether the interactive element is present on the map.
    /// </summary>
    public bool IsOnMap { get; }

    /// <summary>
    /// Gets the map where the interactive element is located.
    /// </summary>
    public Map Map { get; }

    /// <summary>
    /// Gets the map point associated with the interactive element.
    /// </summary>
    public MapPoint Point { get; }

    /// <summary>
    /// Gets or sets the item being gathered by the interactive element.
    /// </summary>
    public int GatheringItem { get; set; }

    /// <summary>
    /// Gets a value indicating whether the interactive element is currently gathering an item.
    /// </summary>
    public bool IsGathering =>
        GatheringItem is not -1;

    /// <summary>
    /// Gets the list of enabled skills for the interactive element.
    /// </summary>
    public List<SkillWrapper> EnabledSkills { get; }

    /// <summary>
    /// Gets the list of disabled skills for the interactive element.
    /// </summary>
    public List<SkillWrapper> DisabledSkills { get; }

    /// <summary>
    /// Gets the current state of the interactive element.
    /// </summary>
    public InteractiveStates State { get; }

    /// <summary>
    /// Gets or sets the timer for using the interactive element.
    /// </summary>
    public Timer? UseTimer { get; set; }

    /// <summary>
    /// Gets or sets the timer for regrowing the interactive element.
    /// </summary>
    public Timer? RegrowTimer { get; set; }

    /// <summary>
    /// Gets or sets the character currently using the interactive element.
    /// </summary>
    public CharacterActor? UsedBy { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="InteractiveWrapper"/> class.
    /// </summary>
    /// <param name="record">The record associated with the interactive element.</param>
    /// <param name="map">The map where the interactive element is located.</param>
    /// <param name="interactiveMapData">The map data for the interactive element.</param>
    /// <param name="actions">The list of actions associated with the interactive element.</param>
    public InteractiveWrapper(InteractiveRecord record, Map map, InteractiveMapData interactiveMapData, List<InteractiveActionRecord> actions)
    {
        Record = record;
        Actions = actions;
        ElementId = record.Id;
        ElementSkillId = record.ElementId;
        GfxId = record.GfxId;
        Animated = record.Animated;
        CellId = interactiveMapData.CellId;
        IsOnMap = interactiveMapData.OnMap;
        Map = map;
        Point = new MapPoint(CellId);
        EnabledSkills = [];
        DisabledSkills = [];
        State = InteractiveStates.Normal;
        ElementTypeId = -1;
        GatheringItem = -1;

        if (!IsOnMap)
            return;

        foreach (var action in actions)
        {
            if (action.InteractiveTemplateId > 0 && ElementTypeId is -1)
                ElementTypeId = action.InteractiveTemplateId;

            switch (action.Type)
            {
                case GameActionTypes.Gathering:
                    GatheringItem = int.Parse(action.Parameters![0], CultureInfo.InvariantCulture);
                    break;

                case GameActionTypes.Zaap:
                    ElementTypeId = (int)InteractiveIds.Zaap;
                    Map.Zaap = this;
                    break;

                case GameActionTypes.Zaapi:
                    ElementTypeId = (int)InteractiveIds.Zaapi;
                    Map.Zaapi = this;
                    break;

                case GameActionTypes.OpenPaddock:
                    ElementTypeId = (int)InteractiveIds.Enclos;
                    Map.Paddock = this;
                    break;
            }
        }

        var isZaap = actions.Any(x => x.InteractiveTemplateId is (int)InteractiveIds.Zaap);
        var isGathering = actions.Any(x => x.Type is GameActionTypes.Gathering);

        foreach (var action in actions.DistinctBy(static x => x.Parameters))
        {
            if (isZaap)
            {
                if (action.SkillId is not ((int)SkillIds.Sauvegarder or (int)SkillIds.Utiliser_114))
                    continue;
            }
            else if (isGathering)
            {
                if (action.Action?.Type is not GameActionTypes.Gathering)
                    continue;
            }

            EnabledSkills.Add(new SkillWrapper(action.Id, (uint)action.SkillId, action));
        }

        EnabledSkills = EnabledSkills.OrderByDescending(static x => x.Id).ToList();
    }

    /// <summary>
    /// Gets the interactive element data for the specified character.
    /// </summary>
    /// <param name="character">The character interacting with the element.</param>
    /// <returns>An <see cref="InteractiveElement"/> object representing the interactive element.</returns>
    public InteractiveElement GetInteractiveElement(CharacterActor character)
    {
        var enabledSkills = EnabledSkills
            //.Where(x => x.Action?.Action?.CanBeExecuted(character) ?? false)
            .Select(static x => x.GetInteractiveElementSkill());

        var disabledSkills = DisabledSkills
            .Where(x => x.Action?.Action?.CanBeExecuted(character) ?? false)
            .Select(static x => x.GetInteractiveElementSkill());

        return IsGathering
            ? new InteractiveElementWithAgeBonus
            {
                ElementId = ElementId,
                ElementTypeId = ElementTypeId,
                EnabledSkills = enabledSkills,
                DisabledSkills = disabledSkills,
                AgeBonus = 0, // TODO: Implement age bonus logic
            }
            : new InteractiveElement
            {
                ElementId = ElementId,
                ElementTypeId = ElementTypeId,
                EnabledSkills = enabledSkills,
                DisabledSkills = disabledSkills
            };
    }

    /// <summary>
    /// Gets the stated element data for the interactive element.
    /// </summary>
    /// <returns>A <see cref="StatedElement"/> object representing the stated element.</returns>
    public StatedElement GetStatedElement()
    {
        return new StatedElement
        {
            ElementId = ElementId,
            ElementCellId = (ushort)CellId,
            ElementState = (uint)State
        };
    }
}
