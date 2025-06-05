// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Context.Roleplay.Paddock;

public sealed class PaddockToSellFilterMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6161;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static PaddockToSellFilterMessage Empty =>
		new() { AreaId = 0, AtLeastNbMount = 0, AtLeastNbMachine = 0, MaxPrice = 0 };

	public required int AreaId { get; set; }

	public required sbyte AtLeastNbMount { get; set; }

	public required sbyte AtLeastNbMachine { get; set; }

	public required uint MaxPrice { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteInt32(AreaId);
		writer.WriteInt8(AtLeastNbMount);
		writer.WriteInt8(AtLeastNbMachine);
		writer.WriteVarUInt32(MaxPrice);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		AreaId = reader.ReadInt32();
		AtLeastNbMount = reader.ReadInt8();
		AtLeastNbMachine = reader.ReadInt8();
		MaxPrice = reader.ReadVarUInt32();
	}
}
