// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using System.IO.Pipelines;
using System.Net;
using Microsoft.AspNetCore.Connections;
using Microsoft.AspNetCore.Http.Features;

namespace Krosmoz.Core.Network.Internal;

/// <summary>
/// Represents a connection context that integrates a delegate for middleware execution.
/// Extends the <see cref="ConnectionContext"/> class.
/// </summary>
internal sealed class ConnectionContextWithDelegate : ConnectionContext
{
    private readonly ConnectionContext _context;
    private readonly ConnectionDelegate _connectionDelegate;
    private readonly TaskCompletionSource<object?> _executionTcs;

    private Task _middlewareTask;

    /// <summary>
    /// Gets or sets the task completion source for initialization.
    /// </summary>
    public TaskCompletionSource<ConnectionContext> InitializedTcs { get; }

    /// <summary>
    /// Gets the task representing the execution of the middleware.
    /// </summary>
    public Task ExecutionTask =>
        _executionTcs.Task;

    /// <summary>
    /// Gets or sets the connection ID.
    /// </summary>
    public override string ConnectionId
    {
        get => _context.ConnectionId;
        set => _context.ConnectionId = value;
    }

    /// <summary>
    /// Gets the feature collection associated with the connection.
    /// </summary>
    public override IFeatureCollection Features =>
        _context.Features;

    /// <summary>
    /// Gets or sets the items dictionary for the connection.
    /// </summary>
    public override IDictionary<object, object?> Items
    {
        get => _context.Items;
        set => _context.Items = value;
    }

    /// <summary>
    /// Gets or sets the transport pipe for the connection.
    /// </summary>
    public override IDuplexPipe Transport
    {
        get => _context.Transport;
        set => _context.Transport = value;
    }

    /// <summary>
    /// Gets or sets the local endpoint of the connection.
    /// </summary>
    public override EndPoint? LocalEndPoint
    {
        get => _context.LocalEndPoint;
        set => _context.LocalEndPoint = value;
    }

    /// <summary>
    /// Gets or sets the remote endpoint of the connection.
    /// </summary>
    public override EndPoint? RemoteEndPoint
    {
        get => _context.RemoteEndPoint;
        set => _context.RemoteEndPoint = value;
    }

    /// <summary>
    /// Gets or sets the cancellation token that signals when the connection is closed.
    /// </summary>
    public override CancellationToken ConnectionClosed
    {
        get => _context.ConnectionClosed;
        set => _context.ConnectionClosed = value;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ConnectionContextWithDelegate"/> class.
    /// </summary>
    /// <param name="context">The underlying connection context.</param>
    /// <param name="connectionDelegate">The delegate to execute middleware.</param>
    public ConnectionContextWithDelegate(ConnectionContext context, ConnectionDelegate connectionDelegate)
    {
        _context = context;
        _connectionDelegate = connectionDelegate;
        _executionTcs = new TaskCompletionSource<object?>(TaskCreationOptions.RunContinuationsAsynchronously);
        _middlewareTask = Task.CompletedTask;

        InitializedTcs = new TaskCompletionSource<ConnectionContext>();
    }

    /// <summary>
    /// Starts the middleware execution.
    /// </summary>
    public void Start()
    {
        _middlewareTask = RunMiddlewareAsync();
    }

    /// <summary>
    /// Executes the middleware asynchronously.
    /// </summary>
    private async Task RunMiddlewareAsync()
    {
        try
        {
            await _connectionDelegate(this).ConfigureAwait(false);
        }
        catch (Exception e)
        {
            InitializedTcs.TrySetException(e);
        }
    }

    /// <summary>
    /// Aborts the connection.
    /// </summary>
    public override void Abort()
    {
        _context.Abort();
    }

    /// <summary>
    /// Aborts the connection with a specified reason.
    /// </summary>
    /// <param name="abortReason">The reason for aborting the connection.</param>
    public override void Abort(ConnectionAbortedException abortReason)
    {
        _context.Abort(abortReason);
    }

    /// <summary>
    /// Disposes the connection asynchronously.
    /// </summary>
    /// <returns>A task representing the asynchronous dispose operation.</returns>
    public override async ValueTask DisposeAsync()
    {
        await _context.DisposeAsync().ConfigureAwait(false);

        _executionTcs.TrySetResult(null);

        await _middlewareTask.ConfigureAwait(false);
    }
}
