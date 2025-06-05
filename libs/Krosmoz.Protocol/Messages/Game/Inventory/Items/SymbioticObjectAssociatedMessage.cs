// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Inventory.Items;

public class SymbioticObjectAssociatedMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6527;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static SymbioticObjectAssociatedMessage Empty =>
		new() { HostUID = 0 };

	public required uint HostUID { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteVarUInt32(HostUID);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		HostUID = reader.ReadVarUInt32();
	}
}
