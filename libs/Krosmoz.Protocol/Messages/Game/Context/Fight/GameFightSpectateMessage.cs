// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Game.Action.Fight;
using Krosmoz.Protocol.Types.Game.Actions.Fight;

namespace Krosmoz.Protocol.Messages.Game.Context.Fight;

public class GameFightSpectateMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6069;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static GameFightSpectateMessage Empty =>
		new() { Effects = [], Marks = [], GameTurn = 0, FightStart = 0, Idols = [] };

	public required IEnumerable<FightDispellableEffectExtendedInformations> Effects { get; set; }

	public required IEnumerable<GameActionMark> Marks { get; set; }

	public required ushort GameTurn { get; set; }

	public required int FightStart { get; set; }

	public required IEnumerable<Types.Game.Idol.Idol> Idols { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		var effectsBefore = writer.Position;
		var effectsCount = 0;
		writer.WriteInt16(0);
		foreach (var item in Effects)
		{
			item.Serialize(writer);
			effectsCount++;
		}
		var effectsAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, effectsBefore);
		writer.WriteInt16((short)effectsCount);
		writer.Seek(SeekOrigin.Begin, effectsAfter);
		var marksBefore = writer.Position;
		var marksCount = 0;
		writer.WriteInt16(0);
		foreach (var item in Marks)
		{
			item.Serialize(writer);
			marksCount++;
		}
		var marksAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, marksBefore);
		writer.WriteInt16((short)marksCount);
		writer.Seek(SeekOrigin.Begin, marksAfter);
		writer.WriteVarUInt16(GameTurn);
		writer.WriteInt32(FightStart);
		var idolsBefore = writer.Position;
		var idolsCount = 0;
		writer.WriteInt16(0);
		foreach (var item in Idols)
		{
			item.Serialize(writer);
			idolsCount++;
		}
		var idolsAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, idolsBefore);
		writer.WriteInt16((short)idolsCount);
		writer.Seek(SeekOrigin.Begin, idolsAfter);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		var effectsCount = reader.ReadInt16();
		var effects = new FightDispellableEffectExtendedInformations[effectsCount];
		for (var i = 0; i < effectsCount; i++)
		{
			var entry = FightDispellableEffectExtendedInformations.Empty;
			entry.Deserialize(reader);
			effects[i] = entry;
		}
		Effects = effects;
		var marksCount = reader.ReadInt16();
		var marks = new GameActionMark[marksCount];
		for (var i = 0; i < marksCount; i++)
		{
			var entry = GameActionMark.Empty;
			entry.Deserialize(reader);
			marks[i] = entry;
		}
		Marks = marks;
		GameTurn = reader.ReadVarUInt16();
		FightStart = reader.ReadInt32();
		var idolsCount = reader.ReadInt16();
		var idols = new Types.Game.Idol.Idol[idolsCount];
		for (var i = 0; i < idolsCount; i++)
		{
			var entry = Types.Game.Idol.Idol.Empty;
			entry.Deserialize(reader);
			idols[i] = entry;
		}
		Idols = idols;
	}
}
