// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Inventory.Items;

public class SymbioticObjectErrorMessage : ObjectErrorMessage
{
	public new const uint StaticProtocolId = 6526;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static new SymbioticObjectErrorMessage Empty =>
		new() { Reason = 0, ErrorCode = 0 };

	public required sbyte ErrorCode { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		writer.WriteInt8(ErrorCode);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		ErrorCode = reader.ReadInt8();
	}
}
