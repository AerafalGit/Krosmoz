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
		writer.WriteUtfPrefixedLength16(Name);
		writer.WriteInt16(WorldX);
		writer.WriteInt16(WorldY);
		writer.WriteUtfPrefixedLength16(Sterilizator);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		Name = reader.ReadUtfPrefixedLength16();
		WorldX = reader.ReadInt16();
		WorldY = reader.ReadInt16();
		Sterilizator = reader.ReadUtfPrefixedLength16();
	}
}
