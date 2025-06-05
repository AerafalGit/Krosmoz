// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Enums;

public enum GameActionTypes
{
    Unknown = -1,
    RunToCell,
    Teleport,
    Zaap,
    ZaapSave,
    Zaapi,
    Gathering,
    NpcDialogCreation,
    NpcDialogChangeMessage,
    TeleportGroupToPlayer,
    TeleportUseItem,
    TeleportDungeonUseKey,
    TeleportDungeonUseKeyRing,
    TeleportDungeonComeBack,
    SaveDungeonProgress,
    RemoveDungeonProgress,
    OpenBidHouseBuy,
    BreakItems,
    OpenCraft,
    OpenSmithMagic,
    OpenPaddock,
    ValidateObjective,
    StartQuest,
    NpcDialogLeave,
    AddItem,
    AddItemBank
}
