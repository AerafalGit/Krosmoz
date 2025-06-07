// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Servers.GameServer.Models.Actors.Characters;

namespace Krosmoz.Servers.GameServer.Models.Characteristics;

/// <summary>
/// Represents a delegate for calculating characteristic formulas based on a character.
/// </summary>
/// <param name="character">The character for which the characteristic formula is calculated.</param>
/// <returns>An integer representing the calculated characteristic value.</returns>
public delegate int CharacteristicFormulas(CharacterActor character);
