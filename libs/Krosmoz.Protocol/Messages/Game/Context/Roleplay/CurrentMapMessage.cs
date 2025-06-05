// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Context.Roleplay;

public sealed class CurrentMapMessage : DofusMessage
{
	public new const uint StaticProtocolId = 220;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static CurrentMapMessage Empty =>
		new() { MapId = 0, MapKey = string.Empty };

	public required int MapId { get; set; }

	public required string MapKey { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteInt32(MapId);
		writer.WriteUtfPrefixedLength16(MapKey);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		MapId = reader.ReadInt32();
		MapKey = reader.ReadUtfPrefixedLength16();
	}
}
