// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Inventory.Exchanges;

public sealed class ExchangeMountSterilizeFromPaddockMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6056;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static ExchangeMountSterilizeFromPaddockMessage Empty =>
		new() { Name = string.Empty, WorldX = 0, WorldY = 0, Sterilizator = string.Empty };

	public required string Name { get; set; }

	public required short WorldX { get; set; }

	public required short WorldY { get; set; }

	public required string Sterilizator { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteUtfLengthPrefixed16(Name);
		writer.WriteShort(WorldX);
		writer.WriteShort(WorldY);
		writer.WriteUtfLengthPrefixed16(Sterilizator);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		Name = reader.ReadUtfLengthPrefixed16();
		WorldX = reader.ReadShort();
		WorldY = reader.ReadShort();
		Sterilizator = reader.ReadUtfLengthPrefixed16();
	}
}
