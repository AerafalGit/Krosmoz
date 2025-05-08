// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Inventory.Exchanges;

public sealed class ExchangeMountTakenFromPaddockMessage : DofusMessage
{
	public new const uint StaticProtocolId = 5994;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static ExchangeMountTakenFromPaddockMessage Empty =>
		new() { Name = string.Empty, WorldX = 0, WorldY = 0, Ownername = string.Empty };

	public required string Name { get; set; }

	public required short WorldX { get; set; }

	public required short WorldY { get; set; }

	public required string Ownername { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteUtfLengthPrefixed16(Name);
		writer.WriteShort(WorldX);
		writer.WriteShort(WorldY);
		writer.WriteUtfLengthPrefixed16(Ownername);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		Name = reader.ReadUtfLengthPrefixed16();
		WorldX = reader.ReadShort();
		WorldY = reader.ReadShort();
		Ownername = reader.ReadUtfLengthPrefixed16();
	}
}
