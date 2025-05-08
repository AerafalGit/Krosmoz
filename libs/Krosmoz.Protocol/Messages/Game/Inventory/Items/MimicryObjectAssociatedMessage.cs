// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Inventory.Items;

public sealed class MimicryObjectAssociatedMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6462;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static MimicryObjectAssociatedMessage Empty =>
		new() { HostUID = 0 };

	public required int HostUID { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteInt(HostUID);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		HostUID = reader.ReadInt();
	}
}
