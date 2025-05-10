// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Context.Fight;

public sealed class GameFightJoinMessage : DofusMessage
{
	public new const uint StaticProtocolId = 702;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static GameFightJoinMessage Empty =>
		new() { CanBeCancelled = false, CanSayReady = false, IsSpectator = false, IsFightStarted = false, TimeMaxBeforeFightStart = 0, FightType = 0 };

	public required bool CanBeCancelled { get; set; }

	public required bool CanSayReady { get; set; }

	public required bool IsSpectator { get; set; }

	public required bool IsFightStarted { get; set; }

	public required int TimeMaxBeforeFightStart { get; set; }

	public required sbyte FightType { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		var flag = new byte();
		flag = BooleanByteWrapper.SetFlag(flag, 0, CanBeCancelled);
		flag = BooleanByteWrapper.SetFlag(flag, 1, CanSayReady);
		flag = BooleanByteWrapper.SetFlag(flag, 2, IsSpectator);
		flag = BooleanByteWrapper.SetFlag(flag, 3, IsFightStarted);
		writer.WriteByte(flag);
		writer.WriteInt(TimeMaxBeforeFightStart);
		writer.WriteSByte(FightType);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		var flag = reader.ReadByte();
		CanBeCancelled = BooleanByteWrapper.GetFlag(flag, 0);
		CanSayReady = BooleanByteWrapper.GetFlag(flag, 1);
		IsSpectator = BooleanByteWrapper.GetFlag(flag, 2);
		IsFightStarted = BooleanByteWrapper.GetFlag(flag, 3);
		TimeMaxBeforeFightStart = reader.ReadInt();
		FightType = reader.ReadSByte();
	}
}
