// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Messages.Game.Inventory.Exchanges;
using Krosmoz.Protocol.Types.Game.Data.Items;

namespace Krosmoz.Protocol.Messages.Game.Inventory.Items;

public sealed class ExchangeObjectPutInBagMessage : ExchangeObjectMessage
{
	public new const uint StaticProtocolId = 6009;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static new ExchangeObjectPutInBagMessage Empty =>
		new() { Remote = false, Object = ObjectItem.Empty };

	public required ObjectItem Object { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		Object.Serialize(writer);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		Object = ObjectItem.Empty;
		Object.Deserialize(reader);
	}
}
