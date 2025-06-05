// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Game.Data.Items;

namespace Krosmoz.Protocol.Messages.Game.Inventory.Exchanges;

public sealed class ExchangeCraftResultMagicWithObjectDescMessage : ExchangeCraftResultWithObjectDescMessage
{
	public new const uint StaticProtocolId = 6188;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static new ExchangeCraftResultMagicWithObjectDescMessage Empty =>
		new() { CraftResult = 0, ObjectInfo = ObjectItemNotInContainer.Empty, MagicPoolStatus = 0 };

	public required sbyte MagicPoolStatus { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		writer.WriteInt8(MagicPoolStatus);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		MagicPoolStatus = reader.ReadInt8();
	}
}
