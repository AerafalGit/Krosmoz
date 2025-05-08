// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Game.Data.Items;

namespace Krosmoz.Protocol.Messages.Game.Inventory.Items;

public sealed class GoldAddedMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6030;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static GoldAddedMessage Empty =>
		new() { Gold = GoldItem.Empty };

	public required GoldItem Gold { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		Gold.Serialize(writer);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		Gold = GoldItem.Empty;
		Gold.Deserialize(reader);
	}
}
