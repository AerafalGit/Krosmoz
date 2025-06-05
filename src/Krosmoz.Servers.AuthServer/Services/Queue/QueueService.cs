// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using System.Collections.Concurrent;
using Krosmoz.Core.Scheduling;
using Krosmoz.Core.Scheduling.Jobs;
using Krosmoz.Core.Services;
using Krosmoz.Protocol.Messages.Queues;
using Krosmoz.Servers.AuthServer.Network.Transport;

namespace Krosmoz.Servers.AuthServer.Services.Queue;

/// <summary>
/// Represents a service that manages a queue of authentication connections.
/// </summary>
public sealed class QueueService : IQueueService, IAsyncInitializableService
{
    private readonly ConcurrentDictionary<DofusConnection, DateTime> _connectionQueue;
    private readonly IScheduler _scheduler;

    /// <summary>
    /// Initializes a new instance of the <see cref="QueueService"/> class.
    /// </summary>
    /// <param name="scheduler">The scheduler used to manage periodic tasks.</param>
    public QueueService(IScheduler scheduler)
    {
        _scheduler = scheduler;
        _connectionQueue = [];
    }

    /// <summary>
    /// Adds an authentication connection to the queue.
    /// </summary>
    /// <param name="connection">The authentication connection to enqueue.</param>
    public void Enqueue(DofusConnection connection)
    {
        _connectionQueue.TryAdd(connection, DateTime.UtcNow);
    }

    /// <summary>
    /// Removes an authentication connection from the queue.
    /// </summary>
    /// <param name="connection">The authentication connection to dequeue.</param>
    public void Dequeue(DofusConnection connection)
    {
        _connectionQueue.TryRemove(connection, out _);
    }

    /// <summary>
    /// Sends the current queue status to a specific authentication connection.
    /// </summary>
    /// <param name="connection">The authentication connection to send the status to.</param>
    /// <param name="position">The position of the connection in the queue.</param>
    /// <param name="total">The total number of connections in the queue.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public ValueTask SendQueueStatusAsync(DofusConnection connection, ushort position, ushort total)
    {
        return connection.SendAsync(new LoginQueueStatusMessage
        {
            Position = position,
            Total = total
        });
    }

    /// <summary>
    /// Asynchronously initializes the queue service by scheduling the queue processing job.
    /// </summary>
    /// <param name="cancellationToken">A token to signal the initialization should be canceled.</param>
    /// <returns>A task that represents the asynchronous initialization operation.</returns>
    public async Task InitializeAsync(CancellationToken cancellationToken)
    {
        var processQueueJob = Job.CreateBuilder(ProcessQueueAsync)
            .WithName("QueueService::ProcessQueue()")
            .RunAsPeriodically(TimeSpan.FromSeconds(1))
            .Build();

        await _scheduler.EnqueueAsync(processQueueJob);
        await _scheduler.StartAsync();
    }

    /// <summary>
    /// Processes the queue periodically, updating connection statuses and removing disconnected connections.
    /// </summary>
    /// <param name="cancellationToken">A token to signal the operation should be canceled.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    private async Task ProcessQueueAsync(CancellationToken cancellationToken)
    {
        var queue = _connectionQueue.ToArray();

        for (var index = 0; index < queue.Length; index++)
        {
            var (connection, enterQueueTime) = queue[index];

            if (!connection.IsConnected)
            {
                Dequeue(connection);
                continue;
            }

            if (DateTime.UtcNow - enterQueueTime >= TimeSpan.FromSeconds(30))
            {
                Dequeue(connection);
                connection.Abort("Queue timeout exceeded.");
                continue;
            }

            await SendQueueStatusAsync(connection, (ushort)(index + 1), (ushort)queue.Length);
        }
    }
}
