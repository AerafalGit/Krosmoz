// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Context.Roleplay.Party;

public sealed class PartyUpdateLightMessage : AbstractPartyEventMessage
{
	public new const uint StaticProtocolId = 6054;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static new PartyUpdateLightMessage Empty =>
		new() { PartyId = 0, Id = 0, LifePoints = 0, MaxLifePoints = 0, Prospecting = 0, RegenRate = 0 };

	public required int Id { get; set; }

	public required int LifePoints { get; set; }

	public required int MaxLifePoints { get; set; }

	public required short Prospecting { get; set; }

	public required byte RegenRate { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		writer.WriteInt(Id);
		writer.WriteInt(LifePoints);
		writer.WriteInt(MaxLifePoints);
		writer.WriteShort(Prospecting);
		writer.WriteByte(RegenRate);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		Id = reader.ReadInt();
		LifePoints = reader.ReadInt();
		MaxLifePoints = reader.ReadInt();
		Prospecting = reader.ReadShort();
		RegenRate = reader.ReadByte();
	}
}
