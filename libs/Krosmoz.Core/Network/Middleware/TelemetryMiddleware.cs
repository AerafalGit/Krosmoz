// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using System.Diagnostics;
using Krosmoz.Core.Network.Server;
using Krosmoz.Core.Network.Transport;
using Microsoft.AspNetCore.Connections;

namespace Krosmoz.Core.Network.Middleware;

/// <summary>
/// Middleware for capturing telemetry data for network connections.
/// It tracks connection metrics, logs activity, and handles errors.
/// </summary>
public sealed class TelemetryMiddleware
{
    /// <summary>
    /// The activity source used for tracing telemetry data.
    /// </summary>
    private static readonly ActivitySource s_activitySource = new("Boufbowl.Network");

    private readonly ConnectionDelegate _next;
    private readonly ServerMetrics _serverMetrics;

    /// <summary>
    /// Initializes a new instance of the <see cref="TelemetryMiddleware"/> class.
    /// </summary>
    /// <param name="next">The next middleware in the pipeline.</param>
    /// <param name="serverMetrics">The server metrics instance for tracking connection statistics.</param>
    public TelemetryMiddleware(ConnectionDelegate next, ServerMetrics serverMetrics)
    {
        _next = next;
        _serverMetrics = serverMetrics;
    }

    /// <summary>
    /// Invokes the middleware to process the connection context.
    /// </summary>
    /// <param name="context">The connection context.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public async Task InvokeAsync(ConnectionContext context)
    {
        using var activity = s_activitySource.CreateActivity("connection.accept", ActivityKind.Internal);

        activity?.SetTag("connection.id", context.ConnectionId);
        activity?.SetTag("connection.remote_endpoint", context.RemoteEndPoint?.ToString());

        var connectionMetrics = new SocketConnectionMetrics(context.ConnectionId, context.RemoteEndPoint);

        context.Features.Set(_serverMetrics);
        context.Features.Set(connectionMetrics);

        var stopwatch = Stopwatch.StartNew();

        _serverMetrics.IncrementConnectionsTotal(context.ConnectionId, context.RemoteEndPoint);

        try
        {
            await _next(context).ConfigureAwait(false);

            activity?.SetStatus(ActivityStatusCode.Ok);
        }
        catch (Exception e)
        {
            connectionMetrics.IncrementErrors(e);

            activity?.SetStatus(ActivityStatusCode.Error);
            activity?.SetTag("error.type", e.GetType().Name);
            activity?.SetTag("error.message", e.Message);
        }
        finally
        {
            stopwatch.Stop();

            _serverMetrics.DecrementConnectionsActive(context.ConnectionId, context.RemoteEndPoint);
            _serverMetrics.RecordConnectionDuration(context.ConnectionId, context.RemoteEndPoint, stopwatch.Elapsed);

            connectionMetrics.Dispose();
        }
    }
}
