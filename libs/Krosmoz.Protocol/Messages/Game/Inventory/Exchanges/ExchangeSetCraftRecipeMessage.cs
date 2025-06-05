// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Inventory.Exchanges;

public sealed class ExchangeSetCraftRecipeMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6389;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static ExchangeSetCraftRecipeMessage Empty =>
		new() { ObjectGID = 0 };

	public required ushort ObjectGID { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteVarUInt16(ObjectGID);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		ObjectGID = reader.ReadVarUInt16();
	}
}
