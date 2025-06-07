// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Servers.GameServer.Services.Experiences;

/// <summary>
/// Defines the contract for a service that provides operations related to experience levels for various entities.
/// </summary>
public interface IExperienceService
{
    /// <summary>
    /// Gets the highest character level available.
    /// </summary>
    byte HighestCharacterLevel { get; }

    /// <summary>
    /// Gets the highest guild level available.
    /// </summary>
    byte HighestGuildLevel { get; }

    /// <summary>
    /// Gets the highest mount level available.
    /// </summary>
    byte HighestMountLevel { get; }

    /// <summary>
    /// Gets the highest job level available.
    /// </summary>
    byte HighestJobLevel { get; }

    /// <summary>
    /// Gets the highest alignment grade available.
    /// </summary>
    byte HighestAlignmentGrade { get; }

    /// <summary>
    /// Retrieves the experience required for a character to reach a specific level.
    /// </summary>
    /// <param name="level">The character level.</param>
    /// <returns>The experience required for the specified level.</returns>
    ulong GetCharacterExperienceByLevel(byte level);

    /// <summary>
    /// Retrieves the experience required for a character to reach the next level.
    /// </summary>
    /// <param name="level">The current character level.</param>
    /// <returns>The experience required for the next level.</returns>
    ulong GetCharacterNextExperienceByLevel(byte level);

    /// <summary>
    /// Determines the character level based on the given experience points.
    /// </summary>
    /// <param name="experience">The experience points.</param>
    /// <returns>The character level corresponding to the experience points.</returns>
    byte GetCharacterLevelByExperience(ulong experience);

    /// <summary>
    /// Retrieves the experience required for a guild to reach a specific level.
    /// </summary>
    /// <param name="level">The guild level.</param>
    /// <returns>The experience required for the specified level.</returns>
    ulong GetGuildExperienceByLevel(byte level);

    /// <summary>
    /// Retrieves the experience required for a guild to reach the next level.
    /// </summary>
    /// <param name="level">The current guild level.</param>
    /// <returns>The experience required for the next level.</returns>
    ulong GetGuildNextExperienceByLevel(byte level);

    /// <summary>
    /// Determines the guild level based on the given experience points.
    /// </summary>
    /// <param name="experience">The experience points.</param>
    /// <returns>The guild level corresponding to the experience points.</returns>
    byte GetGuildLevelByExperience(ulong experience);

    /// <summary>
    /// Retrieves the experience required for a mount to reach a specific level.
    /// </summary>
    /// <param name="level">The mount level.</param>
    /// <returns>The experience required for the specified level.</returns>
    ulong GetMountExperienceByLevel(byte level);

    /// <summary>
    /// Retrieves the experience required for a mount to reach the next level.
    /// </summary>
    /// <param name="level">The current mount level.</param>
    /// <returns>The experience required for the next level.</returns>
    ulong GetMountNextExperienceByLevel(byte level);

    /// <summary>
    /// Determines the mount level based on the given experience points.
    /// </summary>
    /// <param name="experience">The experience points.</param>
    /// <returns>The mount level corresponding to the experience points.</returns>
    byte GetMountLevelByExperience(ulong experience);

    /// <summary>
    /// Retrieves the experience required for a job to reach a specific level.
    /// </summary>
    /// <param name="level">The job level.</param>
    /// <returns>The experience required for the specified level.</returns>
    ulong GetJobExperienceByLevel(byte level);

    /// <summary>
    /// Retrieves the experience required for a job to reach the next level.
    /// </summary>
    /// <param name="level">The current job level.</param>
    /// <returns>The experience required for the next level.</returns>
    ulong GetJobNextExperienceByLevel(byte level);

    /// <summary>
    /// Determines the job level based on the given experience points.
    /// </summary>
    /// <param name="experience">The experience points.</param>
    /// <returns>The job level corresponding to the experience points.</returns>
    byte GetJobLevelByExperience(ulong experience);

    /// <summary>
    /// Retrieves the experience required for an alignment to reach a specific grade.
    /// </summary>
    /// <param name="grade">The alignment grade.</param>
    /// <returns>The experience required for the specified grade.</returns>
    ulong GetAlignmentExperienceByGrade(byte grade);

    /// <summary>
    /// Retrieves the experience required for an alignment to reach the next grade.
    /// </summary>
    /// <param name="grade">The current alignment grade.</param>
    /// <returns>The experience required for the next grade.</returns>
    ulong GetAlignmentNextExperienceByGrade(byte grade);

    /// <summary>
    /// Determines the alignment grade based on the given experience points.
    /// </summary>
    /// <param name="experience">The experience points.</param>
    /// <returns>The alignment grade corresponding to the experience points.</returns>
    byte GetAlignmentGradeByExperience(ulong experience);
}
