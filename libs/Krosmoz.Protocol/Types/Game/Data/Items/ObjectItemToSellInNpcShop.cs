// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Types.Game.Data.Items;

public sealed class ObjectItemToSellInNpcShop : ObjectItemMinimalInformation
{
	public new const ushort StaticProtocolId = 352;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static new ObjectItemToSellInNpcShop Empty =>
		new() { Effects = [], ObjectGID = 0, ObjectPrice = 0, BuyCriterion = string.Empty };

	public required uint ObjectPrice { get; set; }

	public required string BuyCriterion { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		writer.WriteVarUInt32(ObjectPrice);
		writer.WriteUtfPrefixedLength16(BuyCriterion);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		ObjectPrice = reader.ReadVarUInt32();
		BuyCriterion = reader.ReadUtfPrefixedLength16();
	}
}
