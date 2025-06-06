// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Ipc.Messages.Accounts;

public sealed class IpcAccountByUsernameAndPasswordRequest
{
    public required string Username { get; init; }

    public required string Password { get; init; }
}
