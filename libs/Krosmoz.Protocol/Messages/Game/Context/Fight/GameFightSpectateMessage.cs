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
		new() { Effects = [], Marks = [], GameTurn = 0 };

	public required IEnumerable<FightDispellableEffectExtendedInformations> Effects { get; set; }

	public required IEnumerable<GameActionMark> Marks { get; set; }

	public required short GameTurn { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		var effectsBefore = writer.Position;
		var effectsCount = 0;
		writer.WriteShort(0);
		foreach (var item in Effects)
		{
			item.Serialize(writer);
			effectsCount++;
		}
		var effectsAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, effectsBefore);
		writer.WriteShort((short)effectsCount);
		writer.Seek(SeekOrigin.Begin, effectsAfter);
		var marksBefore = writer.Position;
		var marksCount = 0;
		writer.WriteShort(0);
		foreach (var item in Marks)
		{
			item.Serialize(writer);
			marksCount++;
		}
		var marksAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, marksBefore);
		writer.WriteShort((short)marksCount);
		writer.Seek(SeekOrigin.Begin, marksAfter);
		writer.WriteShort(GameTurn);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		var effectsCount = reader.ReadShort();
		var effects = new FightDispellableEffectExtendedInformations[effectsCount];
		for (var i = 0; i < effectsCount; i++)
		{
			var entry = FightDispellableEffectExtendedInformations.Empty;
			entry.Deserialize(reader);
			effects[i] = entry;
		}
		Effects = effects;
		var marksCount = reader.ReadShort();
		var marks = new GameActionMark[marksCount];
		for (var i = 0; i < marksCount; i++)
		{
			var entry = GameActionMark.Empty;
			entry.Deserialize(reader);
			marks[i] = entry;
		}
		Marks = marks;
		GameTurn = reader.ReadShort();
	}
}
