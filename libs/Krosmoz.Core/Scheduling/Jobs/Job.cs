// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Core.Scheduling.Jobs;

/// <summary>
/// Represents a job that can be scheduled for execution with optional conditions and delays.
/// </summary>
public sealed class Job
{
    private readonly Func<CancellationToken, Task> _asyncCallback;
    private readonly Func<bool>? _predicate;

    /// <summary>
    /// Gets the scheduled execution time of the job.
    /// </summary>
    public DateTime Time { get; private set; }

    /// <summary>
    /// Gets or sets a value indicating whether the job is canceled.
    /// </summary>
    public bool IsCancelled { get; set; }

    /// <summary>
    /// Gets a value indicating whether the job is configured to run after a delay.
    /// </summary>
    public bool IsDelayed { get; }

    /// <summary>
    /// Gets a value indicating whether the job is configured to run periodically.
    /// </summary>
    public bool IsPeriodic { get; }

    /// <summary>
    /// Gets the name of the job.
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// Gets the delay duration for the job.
    /// </summary>
    public TimeSpan Delay { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="Job"/> class.
    /// </summary>
    /// <param name="name">The name of the job.</param>
    /// <param name="asyncCallback">The asynchronous callback to execute for the job.</param>
    /// <param name="delay">The delay duration before the job executes.</param>
    /// <param name="isDelayed">Indicates whether the job is delayed.</param>
    /// <param name="isPeriodic">Indicates whether the job is periodic.</param>
    /// <param name="predicate">The optional condition to determine if the job should execute.</param>
    public Job(string name, Func<CancellationToken, Task> asyncCallback, TimeSpan delay, bool isDelayed, bool isPeriodic, Func<bool>? predicate)
    {
        _asyncCallback = asyncCallback;
        _predicate = predicate;

        Name = name;
        Delay = delay;
        Time = DateTime.Now + Delay;
        IsDelayed = isDelayed;
        IsPeriodic = isPeriodic;
    }

    /// <summary>
    /// Determines whether the job is available for execution based on its condition and scheduled time.
    /// </summary>
    /// <returns><c>true</c> if the job is available for execution; otherwise, <c>false</c>.</returns>
    public bool IsAvailable()
    {
        if (_predicate is null)
            return Time <= DateTime.Now;

        if (!_predicate())
            return false;

        return Time <= DateTime.Now;
    }

    /// <summary>
    /// Executes the job asynchronously if it is not canceled.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token to observe during execution.</param>
    /// <returns>A task that represents the asynchronous execution of the job.</returns>
    public async Task ExecuteAsync(CancellationToken cancellationToken = default)
    {
        Time = DateTime.Now + Delay;

        if (!IsCancelled)
            await _asyncCallback(cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Cancels the job, preventing it from being executed.
    /// </summary>
    public void Cancel()
    {
        IsCancelled = true;
    }

    /// <summary>
    /// Creates a new instance of the <see cref="JobBuilder"/> class with the specified asynchronous callback.
    /// </summary>
    /// <param name="asyncCallback">The asynchronous callback to execute for the job.</param>
    /// <returns>A new instance of the <see cref="JobBuilder"/> class.</returns>
    public static JobBuilder CreateBuilder(Func<CancellationToken, Task> asyncCallback)
    {
        return new JobBuilder(asyncCallback);
    }
}
