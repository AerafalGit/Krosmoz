// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Context.Roleplay.Paddock;

public sealed class PaddockToSellListRequestMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6141;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static PaddockToSellListRequestMessage Empty =>
		new() { PageIndex = 0 };

	public required ushort PageIndex { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteVarUInt16(PageIndex);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		PageIndex = reader.ReadVarUInt16();
	}
}
