// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Ipc.Types.Accounts;

namespace Krosmoz.Protocol.Ipc.Messages.Accounts;

public sealed class IpcAccountByUsernameAndPasswordResponse
{
    public IpcAccount? Account { get; init; }
}
