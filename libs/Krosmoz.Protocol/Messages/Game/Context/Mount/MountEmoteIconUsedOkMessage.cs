// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Context.Mount;

public sealed class MountEmoteIconUsedOkMessage : DofusMessage
{
	public new const uint StaticProtocolId = 5978;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static MountEmoteIconUsedOkMessage Empty =>
		new() { MountId = 0, ReactionType = 0 };

	public required int MountId { get; set; }

	public required sbyte ReactionType { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteVarInt32(MountId);
		writer.WriteInt8(ReactionType);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		MountId = reader.ReadVarInt32();
		ReactionType = reader.ReadInt8();
	}
}
