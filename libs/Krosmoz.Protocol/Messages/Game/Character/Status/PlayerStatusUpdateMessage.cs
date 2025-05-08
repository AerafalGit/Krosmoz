// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Game.Character.Status;

namespace Krosmoz.Protocol.Messages.Game.Character.Status;

public sealed class PlayerStatusUpdateMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6386;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static PlayerStatusUpdateMessage Empty =>
		new() { AccountId = 0, PlayerId = 0, Status = PlayerStatus.Empty };

	public required int AccountId { get; set; }

	public required int PlayerId { get; set; }

	public required PlayerStatus Status { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteInt(AccountId);
		writer.WriteInt(PlayerId);
		writer.WriteUShort(Status.ProtocolId);
		Status.Serialize(writer);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		AccountId = reader.ReadInt();
		PlayerId = reader.ReadInt();
		Status = Types.TypeFactory.CreateType<PlayerStatus>(reader.ReadUShort());
		Status.Deserialize(reader);
	}
}
