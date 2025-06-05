// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Inventory.Items;

public class SymbioticObjectAssociateRequestMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6522;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static SymbioticObjectAssociateRequestMessage Empty =>
		new() { SymbioteUID = 0, SymbiotePos = 0, HostUID = 0, HostPos = 0 };

	public required uint SymbioteUID { get; set; }

	public required byte SymbiotePos { get; set; }

	public required uint HostUID { get; set; }

	public required byte HostPos { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteVarUInt32(SymbioteUID);
		writer.WriteUInt8(SymbiotePos);
		writer.WriteVarUInt32(HostUID);
		writer.WriteUInt8(HostPos);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		SymbioteUID = reader.ReadVarUInt32();
		SymbiotePos = reader.ReadUInt8();
		HostUID = reader.ReadVarUInt32();
		HostPos = reader.ReadUInt8();
	}
}
