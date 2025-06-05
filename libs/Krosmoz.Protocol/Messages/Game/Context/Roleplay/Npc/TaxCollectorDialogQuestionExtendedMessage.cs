// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Game.Context.Roleplay;

namespace Krosmoz.Protocol.Messages.Game.Context.Roleplay.Npc;

public class TaxCollectorDialogQuestionExtendedMessage : TaxCollectorDialogQuestionBasicMessage
{
	public new const uint StaticProtocolId = 5615;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static new TaxCollectorDialogQuestionExtendedMessage Empty =>
		new() { GuildInfo = BasicGuildInformations.Empty, MaxPods = 0, Prospecting = 0, Wisdom = 0, TaxCollectorsCount = 0, TaxCollectorAttack = 0, Kamas = 0, Experience = 0, Pods = 0, ItemsValue = 0 };

	public required ushort MaxPods { get; set; }

	public required ushort Prospecting { get; set; }

	public required ushort Wisdom { get; set; }

	public required sbyte TaxCollectorsCount { get; set; }

	public required int TaxCollectorAttack { get; set; }

	public required uint Kamas { get; set; }

	public required ulong Experience { get; set; }

	public required uint Pods { get; set; }

	public required uint ItemsValue { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		writer.WriteVarUInt16(MaxPods);
		writer.WriteVarUInt16(Prospecting);
		writer.WriteVarUInt16(Wisdom);
		writer.WriteInt8(TaxCollectorsCount);
		writer.WriteInt32(TaxCollectorAttack);
		writer.WriteVarUInt32(Kamas);
		writer.WriteVarUInt64(Experience);
		writer.WriteVarUInt32(Pods);
		writer.WriteVarUInt32(ItemsValue);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		MaxPods = reader.ReadVarUInt16();
		Prospecting = reader.ReadVarUInt16();
		Wisdom = reader.ReadVarUInt16();
		TaxCollectorsCount = reader.ReadInt8();
		TaxCollectorAttack = reader.ReadInt32();
		Kamas = reader.ReadVarUInt32();
		Experience = reader.ReadVarUInt64();
		Pods = reader.ReadVarUInt32();
		ItemsValue = reader.ReadVarUInt32();
	}
}
