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

	public required int MaxPrice { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteInt(AreaId);
		writer.WriteSByte(AtLeastNbMount);
		writer.WriteSByte(AtLeastNbMachine);
		writer.WriteInt(MaxPrice);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		AreaId = reader.ReadInt();
		AtLeastNbMount = reader.ReadSByte();
		AtLeastNbMachine = reader.ReadSByte();
		MaxPrice = reader.ReadInt();
	}
}
