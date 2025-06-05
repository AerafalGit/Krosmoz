// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Types.Game.Shortcut;

public class ShortcutObject : Shortcut
{
	public new const ushort StaticProtocolId = 367;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static new ShortcutObject Empty =>
		new() { Slot = 0 };
}
