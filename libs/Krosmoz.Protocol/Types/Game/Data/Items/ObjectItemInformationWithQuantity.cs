// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Types.Game.Data.Items;

public sealed class ObjectItemInformationWithQuantity : ObjectItemMinimalInformation
{
	public new const ushort StaticProtocolId = 387;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static new ObjectItemInformationWithQuantity Empty =>
		new() { Effects = [], ObjectGID = 0, Quantity = 0 };

	public required uint Quantity { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		writer.WriteVarUInt32(Quantity);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		Quantity = reader.ReadVarUInt32();
	}
}
