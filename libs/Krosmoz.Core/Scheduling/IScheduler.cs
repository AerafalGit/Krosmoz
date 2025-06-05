// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Core.Scheduling.Jobs;

namespace Krosmoz.Core.Scheduling;

/// <summary>
/// Defines the interface for a scheduler that manages the execution of jobs.
/// </summary>
public interface IScheduler
{
    /// <summary>
    /// Gets a value indicating whether the scheduler is currently running.
    /// </summary>
    bool IsRunning { get; }

    /// <summary>
    /// Starts the scheduler asynchronously.
    /// </summary>
    /// <returns>A task that represents the asynchronous start operation.</returns>
    Task StartAsync();

    /// <summary>
    /// Enqueues a job for execution.
    /// </summary>
    /// <param name="job">The job to enqueue.</param>
    /// <returns>A task that represents the asynchronous enqueue operation.</returns>
    ValueTask EnqueueAsync(Job job);

    /// <summary>
    /// Stops the scheduler asynchronously.
    /// </summary>
    /// <returns>A task that represents the asynchronous stop operation.</returns>
    Task StopAsync();
}
