// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using System.IO.Pipelines;

namespace Krosmoz.Core.Network.Internal;

/// <summary>
/// Represents a duplex pipe that provides both input and output streams.
/// Implements the <see cref="IDuplexPipe"/> interface.
/// </summary>
/// <param name="Input">The <see cref="PipeReader"/> for reading data.</param>
/// <param name="Output">The <see cref="PipeWriter"/> for writing data.</param>
internal sealed record DuplexPipe(PipeReader Input, PipeWriter Output) : IDuplexPipe
{
    /// <summary>
    /// Creates a pair of connected <see cref="DuplexPipe"/> instances for bidirectional communication.
    /// </summary>
    /// <param name="inputOptions">Options for configuring the input pipe.</param>
    /// <param name="outputOptions">Options for configuring the output pipe.</param>
    /// <returns>A <see cref="DuplexPipePair"/> containing two connected <see cref="DuplexPipe"/> instances.</returns>
    public static DuplexPipePair CreateConnectionPair(PipeOptions inputOptions, PipeOptions outputOptions)
    {
        var input = new Pipe(inputOptions);
        var output = new Pipe(outputOptions);

        var transportToApplication = new DuplexPipe(output.Reader, input.Writer);
        var applicationToTransport = new DuplexPipe(input.Reader, output.Writer);

        return new DuplexPipePair(applicationToTransport, transportToApplication);
    }
}
