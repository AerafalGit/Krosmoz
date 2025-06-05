// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Serialization.D2P;

/// <summary>
/// Represents the state of an entry in a D2P file.
/// </summary>
public enum D2PEntryState
{
    /// <summary>
    /// The entry is in its default state with no modifications.
    /// </summary>
    None,

    /// <summary>
    /// The entry has been modified but not yet saved.
    /// </summary>
    Dirty,

    /// <summary>
    /// The entry has been newly added.
    /// </summary>
    Added,

    /// <summary>
    /// The entry has been removed.
    /// </summary>
    Removed
}
