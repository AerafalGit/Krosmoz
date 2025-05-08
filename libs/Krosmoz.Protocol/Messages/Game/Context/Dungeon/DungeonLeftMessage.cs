// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Context.Dungeon;

public sealed class DungeonLeftMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6149;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static DungeonLeftMessage Empty =>
		new() { DungeonId = 0 };

	public required int DungeonId { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteInt(DungeonId);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		DungeonId = reader.ReadInt();
	}
}
