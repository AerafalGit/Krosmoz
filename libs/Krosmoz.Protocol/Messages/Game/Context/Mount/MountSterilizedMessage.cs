// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Context.Mount;

public sealed class MountSterilizedMessage : DofusMessage
{
	public new const uint StaticProtocolId = 5977;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static MountSterilizedMessage Empty =>
		new() { MountId = 0 };

	public required int MountId { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteVarInt32(MountId);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		MountId = reader.ReadVarInt32();
	}
}
