// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Guest;

public sealed class GuestModeMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6505;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static GuestModeMessage Empty =>
		new() { Active = false };

	public required bool Active { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteBoolean(Active);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		Active = reader.ReadBoolean();
	}
}
