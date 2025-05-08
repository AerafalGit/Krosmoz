// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Inventory.Items;

public sealed class MimicryObjectEraseRequestMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6457;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static MimicryObjectEraseRequestMessage Empty =>
		new() { HostUID = 0, HostPos = 0 };

	public required int HostUID { get; set; }

	public required byte HostPos { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteInt(HostUID);
		writer.WriteByte(HostPos);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		HostUID = reader.ReadInt();
		HostPos = reader.ReadByte();
	}
}
