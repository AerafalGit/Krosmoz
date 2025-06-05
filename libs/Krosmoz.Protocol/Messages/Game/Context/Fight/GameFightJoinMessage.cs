// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Context.Fight;

public class GameFightJoinMessage : DofusMessage
{
	public new const uint StaticProtocolId = 702;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static GameFightJoinMessage Empty =>
		new() { CanBeCancelled = false, CanSayReady = false, IsFightStarted = false, TimeMaxBeforeFightStart = 0, FightType = 0 };

	public required bool CanBeCancelled { get; set; }

	public required bool CanSayReady { get; set; }

	public required bool IsFightStarted { get; set; }

	public required short TimeMaxBeforeFightStart { get; set; }

	public required sbyte FightType { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		var flag = new byte();
		flag = BooleanByteWrapper.SetFlag(flag, 0, CanBeCancelled);
		flag = BooleanByteWrapper.SetFlag(flag, 1, CanSayReady);
		flag = BooleanByteWrapper.SetFlag(flag, 2, IsFightStarted);
		writer.WriteUInt8(flag);
		writer.WriteInt16(TimeMaxBeforeFightStart);
		writer.WriteInt8(FightType);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		var flag = reader.ReadUInt8();
		CanBeCancelled = BooleanByteWrapper.GetFlag(flag, 0);
		CanSayReady = BooleanByteWrapper.GetFlag(flag, 1);
		IsFightStarted = BooleanByteWrapper.GetFlag(flag, 2);
		TimeMaxBeforeFightStart = reader.ReadInt16();
		FightType = reader.ReadInt8();
	}
}
