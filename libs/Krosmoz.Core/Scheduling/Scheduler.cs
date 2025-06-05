// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using System.Threading.Channels;
using Krosmoz.Core.Scheduling.Jobs;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Krosmoz.Core.Scheduling;

/// <summary>
/// Represents a scheduler that manages the execution of jobs.
/// </summary>
public sealed class Scheduler : IScheduler
{
    private readonly Channel<Job> _jobs;
    private readonly CancellationTokenSource _cts;
    private readonly ILogger<Scheduler> _logger;

    private Task? _executeTask;
    private bool _isStarted;
    private bool _isStopped;

    /// <summary>
    /// Gets a value indicating whether the scheduler is currently running.
    /// </summary>
    public bool IsRunning =>
        _isStarted && !_isStopped && _executeTask is not null && !_executeTask.IsCompleted;

    /// <summary>
    /// Initializes a new instance of the <see cref="Scheduler"/> class.
    /// </summary>
    /// <param name="logger">The logger used for logging scheduler-related information.</param>
    /// <param name="lifetime">The application lifetime to link the scheduler's cancellation token.</param>
    public Scheduler(ILogger<Scheduler> logger, IHostApplicationLifetime lifetime)
    {
        _logger = logger;
        _cts = CancellationTokenSource.CreateLinkedTokenSource(lifetime.ApplicationStopping);
        _jobs = Channel.CreateUnbounded<Job>(new UnboundedChannelOptions
        {
            SingleReader = true,
            SingleWriter = true,
            AllowSynchronousContinuations = true
        });
    }

    /// <summary>
    /// Starts the scheduler asynchronously.
    /// </summary>
    /// <returns>A task that represents the asynchronous start operation.</returns>
    public Task StartAsync()
    {
        if (_isStarted)
            return Task.CompletedTask;

        _isStarted = true;

        _executeTask = ExecuteAsync();

        return _executeTask.IsCompleted
            ? _executeTask
            : Task.CompletedTask;
    }

    /// <summary>
    /// Enqueues a job for execution.
    /// </summary>
    /// <param name="job">The job to enqueue.</param>
    /// <returns>A value task that represents the asynchronous enqueue operation.</returns>
    public ValueTask EnqueueAsync(Job job)
    {
        return _jobs.Writer.WriteAsync(job, _cts.Token);
    }

    /// <summary>
    /// Stops the scheduler asynchronously.
    /// </summary>
    /// <returns>A task that represents the asynchronous stop operation.</returns>
    public async Task StopAsync()
    {
        if (_executeTask is null)
            return;

        if (_isStopped)
            return;

        _isStopped = true;

        try
        {
            await _cts.CancelAsync().ConfigureAwait(false);
        }
        finally
        {
            await _executeTask.WaitAsync(_cts.Token).ConfigureAwait(ConfigureAwaitOptions.SuppressThrowing);
        }
    }

    /// <summary>
    /// Executes the jobs in the scheduler asynchronously.
    /// </summary>
    /// <returns>A task that represents the asynchronous execution of jobs.</returns>
    private async Task ExecuteAsync()
    {
        try
        {
            while (await _jobs.Reader.WaitToReadAsync(_cts.Token).ConfigureAwait(false))
            {
                if (_isStopped || _cts.Token.IsCancellationRequested)
                    return;

                var jobs = new List<Job>();
                var hasExecutedJob = false;

                while (_jobs.Reader.TryRead(out var job))
                {
                    if (_isStopped || _cts.Token.IsCancellationRequested)
                        return;

                    if (job.IsAvailable())
                    {
                        if (!job.IsCancelled)
                        {
                            try
                            {
                                await job.ExecuteAsync(_cts.Token).ConfigureAwait(false);
                                hasExecutedJob = true;
                            }
                            catch (Exception e)
                            {
                                _logger.LogError(e, "An error occurred while executing a job {JobName}", job.Name);
                            }
                        }

                        if (job is { IsPeriodic: true, IsCancelled: false })
                            jobs.Add(job);
                    }
                    else if (!job.IsCancelled)
                        jobs.Add(job);
                }

                if (!hasExecutedJob)
                {
                    var nextJobTime = jobs.Where(static x => !x.IsCancelled).Min(static x => x.Time);

                    var delay = nextJobTime > DateTime.UtcNow
                        ? nextJobTime - DateTime.UtcNow
                        : TimeSpan.FromMilliseconds(10);

                    await Task.Delay(delay, _cts.Token).ConfigureAwait(false);
                }

                if (jobs.Count is 0)
                    continue;

                foreach (var job in jobs.Where(static x => !x.IsCancelled))
                    await _jobs.Writer.WriteAsync(job, _cts.Token).ConfigureAwait(false);
            }
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occurred while executing the scheduler");
        }
        finally
        {
            _jobs.Writer.Complete();
        }
    }
}
