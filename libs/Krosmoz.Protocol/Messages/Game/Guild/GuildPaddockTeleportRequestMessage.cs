// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Guild;

public sealed class GuildPaddockTeleportRequestMessage : DofusMessage
{
	public new const uint StaticProtocolId = 5957;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static GuildPaddockTeleportRequestMessage Empty =>
		new() { PaddockId = 0 };

	public required int PaddockId { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteInt32(PaddockId);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		PaddockId = reader.ReadInt32();
	}
}
