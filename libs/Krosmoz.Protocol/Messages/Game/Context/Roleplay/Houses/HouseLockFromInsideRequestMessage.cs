// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Messages.Game.Context.Roleplay.Lockable;

namespace Krosmoz.Protocol.Messages.Game.Context.Roleplay.Houses;

public sealed class HouseLockFromInsideRequestMessage : LockableChangeCodeMessage
{
	public new const uint StaticProtocolId = 5885;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static new HouseLockFromInsideRequestMessage Empty =>
		new() { Code = string.Empty };
}
