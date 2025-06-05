// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Game.Context;

namespace Krosmoz.Protocol.Messages.Game.Atlas.Compass;

public class CompassUpdateMessage : DofusMessage
{
	public new const uint StaticProtocolId = 5591;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static CompassUpdateMessage Empty =>
		new() { Type = 0, Coords = MapCoordinates.Empty };

	public required sbyte Type { get; set; }

	public required MapCoordinates Coords { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteInt8(Type);
		writer.WriteUInt16(Coords.ProtocolId);
		Coords.Serialize(writer);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		Type = reader.ReadInt8();
		Coords = Types.TypeFactory.CreateType<MapCoordinates>(reader.ReadUInt16());
		Coords.Deserialize(reader);
	}
}
