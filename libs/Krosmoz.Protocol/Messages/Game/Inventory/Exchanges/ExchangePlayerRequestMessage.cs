// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Inventory.Exchanges;

public sealed class ExchangePlayerRequestMessage : ExchangeRequestMessage
{
	public new const uint StaticProtocolId = 5773;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static new ExchangePlayerRequestMessage Empty =>
		new() { ExchangeType = 0, Target = 0 };

	public required uint Target { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		writer.WriteVarUInt32(Target);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		Target = reader.ReadVarUInt32();
	}
}
