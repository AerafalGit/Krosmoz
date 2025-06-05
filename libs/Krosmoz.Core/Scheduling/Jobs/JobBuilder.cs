// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Core.Scheduling.Jobs;

/// <summary>
/// Builder class for creating and configuring <see cref="Job"/> instances.
/// </summary>
public sealed class JobBuilder
{
    private readonly Func<CancellationToken, Task> _asyncCallback;

    private TimeSpan _delay;
    private bool _isDelayed;
    private bool _isPeriodic;
    private Func<bool>? _predicate;
    private string? _name;

    /// <summary>
    /// Initializes a new instance of the <see cref="JobBuilder"/> class.
    /// </summary>
    /// <param name="asyncCallback">The asynchronous callback to execute for the job.</param>
    public JobBuilder(Func<CancellationToken, Task> asyncCallback)
    {
        _asyncCallback = asyncCallback;
    }

    /// <summary>
    /// Adds a condition to the job that determines whether it should execute.
    /// </summary>
    /// <param name="predicate">The predicate function to evaluate before executing the job.</param>
    /// <returns>The current <see cref="JobBuilder"/> instance for method chaining.</returns>
    public JobBuilder WithCondition(Func<bool> predicate)
    {
        _predicate = predicate;

        return this;
    }

    /// <summary>
    /// Sets the name of the job.
    /// </summary>
    /// <param name="name">The name to assign to the job.</param>
    /// <returns>The current <see cref="JobBuilder"/> instance for method chaining.</returns>
    public JobBuilder WithName(string name)
    {
        _name = name;
        return this;
    }

    /// <summary>
    /// Configures the job to run after a specified delay.
    /// </summary>
    /// <param name="delay">The delay duration before the job executes.</param>
    /// <returns>The current <see cref="JobBuilder"/> instance for method chaining.</returns>
    public JobBuilder RunAsDelayed(TimeSpan delay)
    {
        _delay = delay;
        _isDelayed  = true;
        _isPeriodic = false;
        return this;
    }

    /// <summary>
    /// Configures the job to run periodically with a specified interval.
    /// </summary>
    /// <param name="delay">The interval duration between job executions.</param>
    /// <returns>The current <see cref="JobBuilder"/> instance for method chaining.</returns>
    public JobBuilder RunAsPeriodically(TimeSpan delay)
    {
        _delay = delay;
        _isPeriodic = true;
        return this;
    }

    /// <summary>
    /// Builds and returns a configured <see cref="Job"/> instance.
    /// </summary>
    /// <returns>A new <see cref="Job"/> instance based on the current configuration.</returns>
    /// <exception cref="Exception">
    /// Thrown if the delay is not set before calling this method.
    /// </exception>
    public Job Build()
    {
        if (_delay == TimeSpan.Zero)
            throw new Exception($"The delay cannot be null, call {nameof(RunAsDelayed)} or {nameof(RunAsPeriodically)} before calling {nameof(Build)}.");

        return new Job(_name ?? _asyncCallback.Method.Name, _asyncCallback, _delay, _isDelayed, _isPeriodic, _predicate);
    }
}
