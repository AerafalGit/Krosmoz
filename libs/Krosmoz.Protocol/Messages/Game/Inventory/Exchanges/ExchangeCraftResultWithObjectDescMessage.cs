// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Game.Data.Items;

namespace Krosmoz.Protocol.Messages.Game.Inventory.Exchanges;

public class ExchangeCraftResultWithObjectDescMessage : ExchangeCraftResultMessage
{
	public new const uint StaticProtocolId = 5999;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static new ExchangeCraftResultWithObjectDescMessage Empty =>
		new() { CraftResult = 0, ObjectInfo = ObjectItemNotInContainer.Empty };

	public required ObjectItemNotInContainer ObjectInfo { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		ObjectInfo.Serialize(writer);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		ObjectInfo = ObjectItemNotInContainer.Empty;
		ObjectInfo.Deserialize(reader);
	}
}
