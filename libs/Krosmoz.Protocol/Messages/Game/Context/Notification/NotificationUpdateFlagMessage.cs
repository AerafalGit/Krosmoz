// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Context.Notification;

public sealed class NotificationUpdateFlagMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6090;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static NotificationUpdateFlagMessage Empty =>
		new() { Index = 0 };

	public required ushort Index { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteVarUInt16(Index);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		Index = reader.ReadVarUInt16();
	}
}
