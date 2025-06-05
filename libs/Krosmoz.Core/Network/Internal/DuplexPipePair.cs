// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using System.IO.Pipelines;

namespace Krosmoz.Core.Network.Internal;

/// <summary>
/// Represents a pair of connected <see cref="IDuplexPipe"/> instances for bidirectional communication.
/// </summary>
/// <param name="Transport">The <see cref="IDuplexPipe"/> used for transport-level communication.</param>
/// <param name="Application">The <see cref="IDuplexPipe"/> used for application-level communication.</param>
internal record struct DuplexPipePair(IDuplexPipe Transport, IDuplexPipe Application);
