// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Context.Roleplay.Party;

public class PartyUpdateLightMessage : AbstractPartyEventMessage
{
	public new const uint StaticProtocolId = 6054;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static new PartyUpdateLightMessage Empty =>
		new() { PartyId = 0, Id = 0, LifePoints = 0, MaxLifePoints = 0, Prospecting = 0, RegenRate = 0 };

	public required uint Id { get; set; }

	public required uint LifePoints { get; set; }

	public required uint MaxLifePoints { get; set; }

	public required ushort Prospecting { get; set; }

	public required byte RegenRate { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		writer.WriteVarUInt32(Id);
		writer.WriteVarUInt32(LifePoints);
		writer.WriteVarUInt32(MaxLifePoints);
		writer.WriteVarUInt16(Prospecting);
		writer.WriteUInt8(RegenRate);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		Id = reader.ReadVarUInt32();
		LifePoints = reader.ReadVarUInt32();
		MaxLifePoints = reader.ReadVarUInt32();
		Prospecting = reader.ReadVarUInt16();
		RegenRate = reader.ReadUInt8();
	}
}
