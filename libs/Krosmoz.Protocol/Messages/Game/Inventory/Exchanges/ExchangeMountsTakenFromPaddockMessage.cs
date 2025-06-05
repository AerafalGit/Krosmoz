// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Inventory.Exchanges;

public sealed class ExchangeMountsTakenFromPaddockMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6554;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static ExchangeMountsTakenFromPaddockMessage Empty =>
		new() { Name = string.Empty, WorldX = 0, WorldY = 0, Ownername = string.Empty };

	public required string Name { get; set; }

	public required short WorldX { get; set; }

	public required short WorldY { get; set; }

	public required string Ownername { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteUtfPrefixedLength16(Name);
		writer.WriteInt16(WorldX);
		writer.WriteInt16(WorldY);
		writer.WriteUtfPrefixedLength16(Ownername);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		Name = reader.ReadUtfPrefixedLength16();
		WorldX = reader.ReadInt16();
		WorldY = reader.ReadInt16();
		Ownername = reader.ReadUtfPrefixedLength16();
	}
}
