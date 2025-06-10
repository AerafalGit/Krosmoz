// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using System.Diagnostics.Metrics;
using System.Net;

namespace Krosmoz.Core.Network.Transport;

/// <summary>
/// Provides metrics for monitoring socket connections, including message processing,
/// message size, and data transfer statistics.
/// </summary>
public sealed class SocketConnectionMetrics : IDisposable
{
    /// <summary>
    /// The name of the meter used for metrics.
    /// </summary>
    public const string MeterName = "Krosmoz.SocketConnection";

    /// <summary>
    /// The version of the meter used for metrics.
    /// </summary>
    public const string MeterVersion = "1.0.0";

    private readonly Meter _meter;
    private readonly Histogram<long> _messageProcessingDuration;
    private readonly Histogram<long> _messageSize;
    private readonly Counter<long> _messageErrors;
    private readonly Counter<long> _messageReceived;
    private readonly Counter<long> _messageSent;

    /// <summary>
    /// Gets the unique identifier for the connection.
    /// </summary>
    public string ConnectionId { get; }

    /// <summary>
    /// Gets the UTC timestamp when the connection was established.
    /// </summary>
    public DateTime ConnectedAt { get; }

    /// <summary>
    /// Gets the remote endpoint of the connection, if available.
    /// </summary>
    public EndPoint? RemoteEndPoint { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="SocketConnectionMetrics"/> class.
    /// </summary>
    /// <param name="connectionId">The unique identifier for the connection.</param>
    /// <param name="endpoint">The remote endpoint of the connection.</param>
    public SocketConnectionMetrics(string connectionId, EndPoint? endpoint)
    {
        ConnectionId = connectionId;
        ConnectedAt = DateTime.UtcNow;
        RemoteEndPoint = endpoint;

        _meter = new Meter(MeterName, MeterVersion);
        _messageProcessingDuration = _meter.CreateHistogram<long>("socket_connection_message_processing_duration", "ms", "Duration of message processing in milliseconds");
        _messageSize = _meter.CreateHistogram<long>("socket_connection_message_size", "bytes", "Size of messages in bytes");
        _messageErrors = _meter.CreateCounter<long>("socket_connection_errors", "errors", "Number of processing errors");
        _messageReceived = _meter.CreateCounter<long>("socket_connection_message_received", "messages", "Number of messages received");
        _messageSent = _meter.CreateCounter<long>("socket_connection_message_sent", "messages", "Number of messages sent");
    }

    /// <summary>
    /// Increments the counter for processing errors and records error details.
    /// </summary>
    /// <param name="error">The exception representing the error, if available.</param>
    public void IncrementErrors(Exception? error)
    {
        var baseTags = GetBaseTags();

        baseTags["error_type"] = error?.GetType().Name;
        baseTags["error_message"] = error?.Message;

        var tags = baseTags.ToArray();

        _messageErrors.Add(1, tags);
    }

    /// <summary>
    /// Increments the counter for received messages and records their size.
    /// </summary>
    /// <param name="messageName">The name of the received message.</param>
    /// <param name="messageSize">The size of the received message in bytes.</param>
    public void IncrementMessageReceived(string messageName, long messageSize)
    {
        var baseTags = GetBaseTags();

        baseTags["message_name"] = messageName;
        baseTags["direction"] = "inbound";

        var tags = baseTags.ToArray();

        _messageReceived.Add(1, tags);

        if (messageSize > 0)
            _messageSize.Record(messageSize, tags);
    }

    /// <summary>
    /// Increments the counter for sent messages and records their size.
    /// </summary>
    /// <param name="messageName">The name of the sent message.</param>
    /// <param name="messageSize">The size of the sent message in bytes.</param>
    public void IncrementMessageSent(string messageName, long messageSize)
    {
        var baseTags = GetBaseTags();

        baseTags["message_name"] = messageName;
        baseTags["direction"] = "outbound";

        var tags = baseTags.ToArray();

        _messageSent.Add(1, tags);

        if (messageSize > 0)
            _messageSize.Record(messageSize, tags);
    }

    /// <summary>
    /// Records the duration of message processing.
    /// </summary>
    /// <param name="duration">The duration of message processing.</param>
    /// <param name="messageName">The name of the processed message.</param>
    public void RecordMessageProcessingDuration(TimeSpan duration, string messageName)
    {
        var baseTags = GetBaseTags();

        baseTags["message_name"] = messageName;

        var tags = baseTags.ToArray();

        _messageProcessingDuration.Record((long)duration.TotalMilliseconds, tags);
    }

    /// <summary>
    /// Generates a dictionary of base tags for metrics.
    /// </summary>
    /// <returns>A dictionary containing base tags.</returns>
    private Dictionary<string, object?> GetBaseTags()
    {
        return new Dictionary<string, object?>
        {
            ["connection_id"] = ConnectionId,
            ["connection_duration_minutes"] = (DateTime.UtcNow - ConnectedAt).TotalMinutes,
            ["remote_endpoint"] = RemoteEndPoint?.ToString(),
        };
    }

    /// <summary>
    /// Disposes the meter and releases resources.
    /// </summary>
    public void Dispose()
    {
        _meter.Dispose();
    }
}
