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

	public required short MaxPods { get; set; }

	public required short Prospecting { get; set; }

	public required short Wisdom { get; set; }

	public required sbyte TaxCollectorsCount { get; set; }

	public required int TaxCollectorAttack { get; set; }

	public required int Kamas { get; set; }

	public required double Experience { get; set; }

	public required int Pods { get; set; }

	public required int ItemsValue { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		writer.WriteShort(MaxPods);
		writer.WriteShort(Prospecting);
		writer.WriteShort(Wisdom);
		writer.WriteSByte(TaxCollectorsCount);
		writer.WriteInt(TaxCollectorAttack);
		writer.WriteInt(Kamas);
		writer.WriteDouble(Experience);
		writer.WriteInt(Pods);
		writer.WriteInt(ItemsValue);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		MaxPods = reader.ReadShort();
		Prospecting = reader.ReadShort();
		Wisdom = reader.ReadShort();
		TaxCollectorsCount = reader.ReadSByte();
		TaxCollectorAttack = reader.ReadInt();
		Kamas = reader.ReadInt();
		Experience = reader.ReadDouble();
		Pods = reader.ReadInt();
		ItemsValue = reader.ReadInt();
	}
}
