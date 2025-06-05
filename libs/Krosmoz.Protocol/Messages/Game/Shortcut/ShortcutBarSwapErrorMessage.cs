// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Shortcut;

public sealed class ShortcutBarSwapErrorMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6226;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static ShortcutBarSwapErrorMessage Empty =>
		new() { Error = 0 };

	public required sbyte Error { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteInt8(Error);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		Error = reader.ReadInt8();
	}
}
