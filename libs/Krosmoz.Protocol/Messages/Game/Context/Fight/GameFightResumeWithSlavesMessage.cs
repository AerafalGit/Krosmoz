// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Game.Context.Fight;

namespace Krosmoz.Protocol.Messages.Game.Context.Fight;

public sealed class GameFightResumeWithSlavesMessage : GameFightResumeMessage
{
	public new const uint StaticProtocolId = 6215;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static new GameFightResumeWithSlavesMessage Empty =>
		new() { Idols = [], FightStart = 0, GameTurn = 0, Marks = [], Effects = [], BombCount = 0, SummonCount = 0, SpellCooldowns = [], SlavesInfo = [] };

	public required IEnumerable<GameFightResumeSlaveInfo> SlavesInfo { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		var slavesInfoBefore = writer.Position;
		var slavesInfoCount = 0;
		writer.WriteInt16(0);
		foreach (var item in SlavesInfo)
		{
			item.Serialize(writer);
			slavesInfoCount++;
		}
		var slavesInfoAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, slavesInfoBefore);
		writer.WriteInt16((short)slavesInfoCount);
		writer.Seek(SeekOrigin.Begin, slavesInfoAfter);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		var slavesInfoCount = reader.ReadInt16();
		var slavesInfo = new GameFightResumeSlaveInfo[slavesInfoCount];
		for (var i = 0; i < slavesInfoCount; i++)
		{
			var entry = GameFightResumeSlaveInfo.Empty;
			entry.Deserialize(reader);
			slavesInfo[i] = entry;
		}
		SlavesInfo = slavesInfo;
	}
}
