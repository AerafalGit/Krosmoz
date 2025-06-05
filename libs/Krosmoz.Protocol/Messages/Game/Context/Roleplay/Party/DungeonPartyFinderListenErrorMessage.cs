// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Context.Roleplay.Party;

public sealed class DungeonPartyFinderListenErrorMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6248;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static DungeonPartyFinderListenErrorMessage Empty =>
		new() { DungeonId = 0 };

	public required ushort DungeonId { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteVarUInt16(DungeonId);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		DungeonId = reader.ReadVarUInt16();
	}
}
