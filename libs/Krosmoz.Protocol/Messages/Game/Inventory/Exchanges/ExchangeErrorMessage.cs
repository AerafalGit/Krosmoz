// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Inventory.Exchanges;

public sealed class ExchangeErrorMessage : DofusMessage
{
	public new const uint StaticProtocolId = 5513;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static ExchangeErrorMessage Empty =>
		new() { ErrorType = 0 };

	public required sbyte ErrorType { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteInt8(ErrorType);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		ErrorType = reader.ReadInt8();
	}
}
