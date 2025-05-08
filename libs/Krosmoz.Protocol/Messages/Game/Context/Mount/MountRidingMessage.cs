// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Context.Mount;

public sealed class MountRidingMessage : DofusMessage
{
	public new const uint StaticProtocolId = 5967;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static MountRidingMessage Empty =>
		new() { IsRiding = false };

	public required bool IsRiding { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteBoolean(IsRiding);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		IsRiding = reader.ReadBoolean();
	}
}
