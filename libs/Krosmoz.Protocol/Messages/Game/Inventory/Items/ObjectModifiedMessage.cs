// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Game.Data.Items;

namespace Krosmoz.Protocol.Messages.Game.Inventory.Items;

public sealed class ObjectModifiedMessage : DofusMessage
{
	public new const uint StaticProtocolId = 3029;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static ObjectModifiedMessage Empty =>
		new() { Object = ObjectItem.Empty };

	public required ObjectItem Object { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		Object.Serialize(writer);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		Object = ObjectItem.Empty;
		Object.Deserialize(reader);
	}
}
