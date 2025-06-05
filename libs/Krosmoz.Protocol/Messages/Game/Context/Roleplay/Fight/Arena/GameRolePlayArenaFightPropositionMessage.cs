// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Context.Roleplay.Fight.Arena;

public sealed class GameRolePlayArenaFightPropositionMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6276;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static GameRolePlayArenaFightPropositionMessage Empty =>
		new() { FightId = 0, AlliesId = [], Duration = 0 };

	public required int FightId { get; set; }

	public required IEnumerable<int> AlliesId { get; set; }

	public required ushort Duration { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteInt32(FightId);
		var alliesIdBefore = writer.Position;
		var alliesIdCount = 0;
		writer.WriteInt16(0);
		foreach (var item in AlliesId)
		{
			writer.WriteInt32(item);
			alliesIdCount++;
		}
		var alliesIdAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, alliesIdBefore);
		writer.WriteInt16((short)alliesIdCount);
		writer.Seek(SeekOrigin.Begin, alliesIdAfter);
		writer.WriteVarUInt16(Duration);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		FightId = reader.ReadInt32();
		var alliesIdCount = reader.ReadInt16();
		var alliesId = new int[alliesIdCount];
		for (var i = 0; i < alliesIdCount; i++)
		{
			alliesId[i] = reader.ReadInt32();
		}
		AlliesId = alliesId;
		Duration = reader.ReadVarUInt16();
	}
}
