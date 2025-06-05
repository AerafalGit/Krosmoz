// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Inventory.Items;

public sealed class ObjectJobAddedMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6014;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static ObjectJobAddedMessage Empty =>
		new() { JobId = 0 };

	public required sbyte JobId { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteInt8(JobId);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		JobId = reader.ReadInt8();
	}
}
