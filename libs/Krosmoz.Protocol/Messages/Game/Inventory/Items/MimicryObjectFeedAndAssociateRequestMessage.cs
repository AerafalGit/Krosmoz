// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Inventory.Items;

public sealed class MimicryObjectFeedAndAssociateRequestMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6460;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static MimicryObjectFeedAndAssociateRequestMessage Empty =>
		new() { MimicryUID = 0, MimicryPos = 0, FoodUID = 0, FoodPos = 0, HostUID = 0, HostPos = 0, Preview = false };

	public required int MimicryUID { get; set; }

	public required byte MimicryPos { get; set; }

	public required int FoodUID { get; set; }

	public required byte FoodPos { get; set; }

	public required int HostUID { get; set; }

	public required byte HostPos { get; set; }

	public required bool Preview { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteInt(MimicryUID);
		writer.WriteByte(MimicryPos);
		writer.WriteInt(FoodUID);
		writer.WriteByte(FoodPos);
		writer.WriteInt(HostUID);
		writer.WriteByte(HostPos);
		writer.WriteBoolean(Preview);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		MimicryUID = reader.ReadInt();
		MimicryPos = reader.ReadByte();
		FoodUID = reader.ReadInt();
		FoodPos = reader.ReadByte();
		HostUID = reader.ReadInt();
		HostPos = reader.ReadByte();
		Preview = reader.ReadBoolean();
	}
}
