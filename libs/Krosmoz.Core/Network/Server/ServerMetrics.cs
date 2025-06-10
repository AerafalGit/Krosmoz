// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using System.Diagnostics.Metrics;
using System.Net;

namespace Krosmoz.Core.Network.Server;

/// <summary>
/// Provides metrics for monitoring server activity, including connection statistics
/// and listener activity.
/// </summary>
public sealed class ServerMetrics : IDisposable
{
    /// <summary>
    /// The name of the meter used for server metrics.
    /// </summary>
    public const string MeterName = "Krosmoz.SocketServer";

    /// <summary>
    /// The version of the meter used for server metrics.
    /// </summary>
    public const string MeterVersion = "1.0.0";

    private readonly Meter _meter;
    private readonly Counter<long> _connectionsTotal;
    private readonly Counter<long> _connectionsActive;
    private readonly Histogram<long> _connectionsDuration;
    private readonly Counter<long> _listenersActive;

    /// <summary>
    /// Initializes a new instance of the <see cref="ServerMetrics"/> class.
    /// </summary>
    public ServerMetrics()
    {
        _meter = new Meter(MeterName, MeterVersion);
        _connectionsTotal = _meter.CreateCounter<long>("socket_server_connections_total", "connections", "Total number of connections accepted by the server");
        _connectionsActive = _meter.CreateCounter<long>("socket_server_connections_active", "connections", "Current number of active connections");
        _connectionsDuration = _meter.CreateHistogram<long>("socket_server_connections_duration", "minutes", "Duration of connections in minutes");
        _listenersActive = _meter.CreateCounter<long>("socket_server_listeners_active", "listeners", "Current number of active listeners");
    }

    /// <summary>
    /// Increments the total and active connection counters for a new connection.
    /// </summary>
    /// <param name="connectionId">The unique identifier for the connection.</param>
    /// <param name="endpoint">The remote endpoint of the connection.</param>
    public void IncrementConnectionsTotal(string connectionId, EndPoint? endpoint)
    {
        var tags = new Dictionary<string, object?>
        {
            ["connection_id"] = connectionId,
            ["remote_endpoint"] = endpoint?.ToString()
        }.ToArray();

        _connectionsTotal.Add(1, tags);
        _connectionsActive.Add(1, tags);
    }

    /// <summary>
    /// Decrements the active connection counter for a closed connection.
    /// </summary>
    /// <param name="connectionId">The unique identifier for the connection.</param>
    /// <param name="endpoint">The remote endpoint of the connection.</param>
    public void DecrementConnectionsActive(string connectionId, EndPoint? endpoint)
    {
        var tags = new Dictionary<string, object?>
        {
            ["connection_id"] = connectionId,
            ["remote_endpoint"] = endpoint?.ToString()
        }.ToArray();

        _connectionsActive.Add(-1, tags);
    }

    /// <summary>
    /// Records the duration of a connection.
    /// </summary>
    /// <param name="connectionId">The unique identifier for the connection.</param>
    /// <param name="endpoint">The remote endpoint of the connection.</param>
    /// <param name="duration">The duration of the connection.</param>
    public void RecordConnectionDuration(string connectionId, EndPoint? endpoint, TimeSpan duration)
    {
        var tags = new Dictionary<string, object?>
        {
            ["connection_id"] = connectionId,
            ["remote_endpoint"] = endpoint?.ToString()
        }.ToArray();

        _connectionsDuration.Record((long)duration.TotalMinutes, tags);
    }

    /// <summary>
    /// Increments the active listener counter for a new listener.
    /// </summary>
    /// <param name="endpoint">The endpoint of the listener.</param>
    public void IncrementListenersActive(EndPoint? endpoint)
    {
        var tags = new Dictionary<string, object?>
        {
            ["endpoint"] = endpoint?.ToString()
        }.ToArray();

        _listenersActive.Add(1, tags);
    }

    /// <summary>
    /// Decrements the active listener counter for a stopped listener.
    /// </summary>
    /// <param name="endpoint">The endpoint of the listener.</param>
    public void DecrementListenersActive(EndPoint? endpoint)
    {
        var tags = new Dictionary<string, object?>
        {
            ["endpoint"] = endpoint?.ToString()
        }.ToArray();

        _listenersActive.Add(-1, tags);
    }

    /// <summary>
    /// Disposes the meter and releases resources.
    /// </summary>
    public void Dispose()
    {
        _meter.Dispose();
    }
}
