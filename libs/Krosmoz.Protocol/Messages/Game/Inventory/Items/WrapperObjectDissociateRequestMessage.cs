// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Inventory.Items;

public sealed class WrapperObjectDissociateRequestMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6524;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static WrapperObjectDissociateRequestMessage Empty =>
		new() { HostUID = 0, HostPos = 0 };

	public required uint HostUID { get; set; }

	public required byte HostPos { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteVarUInt32(HostUID);
		writer.WriteUInt8(HostPos);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		HostUID = reader.ReadVarUInt32();
		HostPos = reader.ReadUInt8();
	}
}
