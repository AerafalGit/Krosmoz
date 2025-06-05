// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Game.Context.Roleplay.TreasureHunt;

namespace Krosmoz.Protocol.Messages.Game.Context.Roleplay.TreasureHunt;

public sealed class TreasureHuntMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6486;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static TreasureHuntMessage Empty =>
		new() { QuestType = 0, StartMapId = 0, KnownStepsList = [], TotalStepCount = 0, CheckPointCurrent = 0, CheckPointTotal = 0, AvailableRetryCount = 0, Flags = [] };

	public required sbyte QuestType { get; set; }

	public required int StartMapId { get; set; }

	public required IEnumerable<TreasureHuntStep> KnownStepsList { get; set; }

	public required sbyte TotalStepCount { get; set; }

	public required uint CheckPointCurrent { get; set; }

	public required uint CheckPointTotal { get; set; }

	public required int AvailableRetryCount { get; set; }

	public required IEnumerable<TreasureHuntFlag> Flags { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteInt8(QuestType);
		writer.WriteInt32(StartMapId);
		var knownStepsListBefore = writer.Position;
		var knownStepsListCount = 0;
		writer.WriteInt16(0);
		foreach (var item in KnownStepsList)
		{
			writer.WriteUInt16(item.ProtocolId);
			item.Serialize(writer);
			knownStepsListCount++;
		}
		var knownStepsListAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, knownStepsListBefore);
		writer.WriteInt16((short)knownStepsListCount);
		writer.Seek(SeekOrigin.Begin, knownStepsListAfter);
		writer.WriteInt8(TotalStepCount);
		writer.WriteVarUInt32(CheckPointCurrent);
		writer.WriteVarUInt32(CheckPointTotal);
		writer.WriteInt32(AvailableRetryCount);
		var flagsBefore = writer.Position;
		var flagsCount = 0;
		writer.WriteInt16(0);
		foreach (var item in Flags)
		{
			item.Serialize(writer);
			flagsCount++;
		}
		var flagsAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, flagsBefore);
		writer.WriteInt16((short)flagsCount);
		writer.Seek(SeekOrigin.Begin, flagsAfter);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		QuestType = reader.ReadInt8();
		StartMapId = reader.ReadInt32();
		var knownStepsListCount = reader.ReadInt16();
		var knownStepsList = new TreasureHuntStep[knownStepsListCount];
		for (var i = 0; i < knownStepsListCount; i++)
		{
			var entry = Types.TypeFactory.CreateType<TreasureHuntStep>(reader.ReadUInt16());
			entry.Deserialize(reader);
			knownStepsList[i] = entry;
		}
		KnownStepsList = knownStepsList;
		TotalStepCount = reader.ReadInt8();
		CheckPointCurrent = reader.ReadVarUInt32();
		CheckPointTotal = reader.ReadVarUInt32();
		AvailableRetryCount = reader.ReadInt32();
		var flagsCount = reader.ReadInt16();
		var flags = new TreasureHuntFlag[flagsCount];
		for (var i = 0; i < flagsCount; i++)
		{
			var entry = TreasureHuntFlag.Empty;
			entry.Deserialize(reader);
			flags[i] = entry;
		}
		Flags = flags;
	}
}
