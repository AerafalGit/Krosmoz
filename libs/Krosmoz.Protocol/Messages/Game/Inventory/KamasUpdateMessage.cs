// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Inventory;

public sealed class KamasUpdateMessage : DofusMessage
{
	public new const uint StaticProtocolId = 5537;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static KamasUpdateMessage Empty =>
		new() { KamasTotal = 0 };

	public required int KamasTotal { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteVarInt32(KamasTotal);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		KamasTotal = reader.ReadVarInt32();
	}
}
