// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Types.Game.Character.Restriction;

public sealed class ActorRestrictionsInformations : DofusType
{
	public new const ushort StaticProtocolId = 204;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static ActorRestrictionsInformations Empty =>
		new() { CantBeAggressed = false, CantBeChallenged = false, CantTrade = false, CantBeAttackedByMutant = false, CantRun = false, ForceSlowWalk = false, CantMinimize = false, CantMove = false, CantAggress = false, CantChallenge = false, CantExchange = false, CantAttack = false, CantChat = false, CantBeMerchant = false, CantUseObject = false, CantUseTaxCollector = false, CantUseInteractive = false, CantSpeakToNPC = false, CantChangeZone = false, CantAttackMonster = false, CantWalk8Directions = false };

	public required bool CantBeAggressed { get; set; }

	public required bool CantBeChallenged { get; set; }

	public required bool CantTrade { get; set; }

	public required bool CantBeAttackedByMutant { get; set; }

	public required bool CantRun { get; set; }

	public required bool ForceSlowWalk { get; set; }

	public required bool CantMinimize { get; set; }

	public required bool CantMove { get; set; }

	public required bool CantAggress { get; set; }

	public required bool CantChallenge { get; set; }

	public required bool CantExchange { get; set; }

	public required bool CantAttack { get; set; }

	public required bool CantChat { get; set; }

	public required bool CantBeMerchant { get; set; }

	public required bool CantUseObject { get; set; }

	public required bool CantUseTaxCollector { get; set; }

	public required bool CantUseInteractive { get; set; }

	public required bool CantSpeakToNPC { get; set; }

	public required bool CantChangeZone { get; set; }

	public required bool CantAttackMonster { get; set; }

	public required bool CantWalk8Directions { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		var flag = new byte();
		flag = BooleanByteWrapper.SetFlag(flag, 0, CantBeAggressed);
		flag = BooleanByteWrapper.SetFlag(flag, 1, CantBeChallenged);
		flag = BooleanByteWrapper.SetFlag(flag, 2, CantTrade);
		flag = BooleanByteWrapper.SetFlag(flag, 3, CantBeAttackedByMutant);
		flag = BooleanByteWrapper.SetFlag(flag, 4, CantRun);
		flag = BooleanByteWrapper.SetFlag(flag, 5, ForceSlowWalk);
		flag = BooleanByteWrapper.SetFlag(flag, 6, CantMinimize);
		flag = BooleanByteWrapper.SetFlag(flag, 7, CantMove);
		writer.WriteUInt8(flag);
		flag = new byte();
		flag = BooleanByteWrapper.SetFlag(flag, 0, CantAggress);
		flag = BooleanByteWrapper.SetFlag(flag, 1, CantChallenge);
		flag = BooleanByteWrapper.SetFlag(flag, 2, CantExchange);
		flag = BooleanByteWrapper.SetFlag(flag, 3, CantAttack);
		flag = BooleanByteWrapper.SetFlag(flag, 4, CantChat);
		flag = BooleanByteWrapper.SetFlag(flag, 5, CantBeMerchant);
		flag = BooleanByteWrapper.SetFlag(flag, 6, CantUseObject);
		flag = BooleanByteWrapper.SetFlag(flag, 7, CantUseTaxCollector);
		writer.WriteUInt8(flag);
		flag = new byte();
		flag = BooleanByteWrapper.SetFlag(flag, 0, CantUseInteractive);
		flag = BooleanByteWrapper.SetFlag(flag, 1, CantSpeakToNPC);
		flag = BooleanByteWrapper.SetFlag(flag, 2, CantChangeZone);
		flag = BooleanByteWrapper.SetFlag(flag, 3, CantAttackMonster);
		flag = BooleanByteWrapper.SetFlag(flag, 4, CantWalk8Directions);
		writer.WriteUInt8(flag);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		var flag = reader.ReadUInt8();
		CantBeAggressed = BooleanByteWrapper.GetFlag(flag, 0);
		CantBeChallenged = BooleanByteWrapper.GetFlag(flag, 1);
		CantTrade = BooleanByteWrapper.GetFlag(flag, 2);
		CantBeAttackedByMutant = BooleanByteWrapper.GetFlag(flag, 3);
		CantRun = BooleanByteWrapper.GetFlag(flag, 4);
		ForceSlowWalk = BooleanByteWrapper.GetFlag(flag, 5);
		CantMinimize = BooleanByteWrapper.GetFlag(flag, 6);
		CantMove = BooleanByteWrapper.GetFlag(flag, 7);
		flag = reader.ReadUInt8();
		CantAggress = BooleanByteWrapper.GetFlag(flag, 0);
		CantChallenge = BooleanByteWrapper.GetFlag(flag, 1);
		CantExchange = BooleanByteWrapper.GetFlag(flag, 2);
		CantAttack = BooleanByteWrapper.GetFlag(flag, 3);
		CantChat = BooleanByteWrapper.GetFlag(flag, 4);
		CantBeMerchant = BooleanByteWrapper.GetFlag(flag, 5);
		CantUseObject = BooleanByteWrapper.GetFlag(flag, 6);
		CantUseTaxCollector = BooleanByteWrapper.GetFlag(flag, 7);
		flag = reader.ReadUInt8();
		CantUseInteractive = BooleanByteWrapper.GetFlag(flag, 0);
		CantSpeakToNPC = BooleanByteWrapper.GetFlag(flag, 1);
		CantChangeZone = BooleanByteWrapper.GetFlag(flag, 2);
		CantAttackMonster = BooleanByteWrapper.GetFlag(flag, 3);
		CantWalk8Directions = BooleanByteWrapper.GetFlag(flag, 4);
	}
}
