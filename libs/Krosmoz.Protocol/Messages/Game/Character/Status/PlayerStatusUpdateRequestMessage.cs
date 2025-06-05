// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Game.Character.Status;

namespace Krosmoz.Protocol.Messages.Game.Character.Status;

public sealed class PlayerStatusUpdateRequestMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6387;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static PlayerStatusUpdateRequestMessage Empty =>
		new() { Status = PlayerStatus.Empty };

	public required PlayerStatus Status { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteUInt16(Status.ProtocolId);
		Status.Serialize(writer);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		Status = Types.TypeFactory.CreateType<PlayerStatus>(reader.ReadUInt16());
		Status.Deserialize(reader);
	}
}
