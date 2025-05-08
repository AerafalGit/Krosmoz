// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Game.Character.Choice;
using Krosmoz.Protocol.Types.Game.Character.Status;
using Krosmoz.Protocol.Types.Game.Look;

namespace Krosmoz.Protocol.Types.Game.Context.Roleplay.Party;

public class PartyMemberInformations : CharacterBaseInformations
{
	public new const ushort StaticProtocolId = 90;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static new PartyMemberInformations Empty =>
		new() { Id = 0, Name = string.Empty, Level = 0, EntityLook = EntityLook.Empty, Sex = false, Breed = 0, LifePoints = 0, MaxLifePoints = 0, Prospecting = 0, RegenRate = 0, Initiative = 0, AlignmentSide = 0, WorldX = 0, WorldY = 0, MapId = 0, SubAreaId = 0, Status = PlayerStatus.Empty };

	public required int LifePoints { get; set; }

	public required int MaxLifePoints { get; set; }

	public required short Prospecting { get; set; }

	public required byte RegenRate { get; set; }

	public required short Initiative { get; set; }

	public required sbyte AlignmentSide { get; set; }

	public required short WorldX { get; set; }

	public required short WorldY { get; set; }

	public required int MapId { get; set; }

	public required short SubAreaId { get; set; }

	public required PlayerStatus Status { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		writer.WriteInt(LifePoints);
		writer.WriteInt(MaxLifePoints);
		writer.WriteShort(Prospecting);
		writer.WriteByte(RegenRate);
		writer.WriteShort(Initiative);
		writer.WriteSByte(AlignmentSide);
		writer.WriteShort(WorldX);
		writer.WriteShort(WorldY);
		writer.WriteInt(MapId);
		writer.WriteShort(SubAreaId);
		writer.WriteUShort(Status.ProtocolId);
		Status.Serialize(writer);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		LifePoints = reader.ReadInt();
		MaxLifePoints = reader.ReadInt();
		Prospecting = reader.ReadShort();
		RegenRate = reader.ReadByte();
		Initiative = reader.ReadShort();
		AlignmentSide = reader.ReadSByte();
		WorldX = reader.ReadShort();
		WorldY = reader.ReadShort();
		MapId = reader.ReadInt();
		SubAreaId = reader.ReadShort();
		Status = Types.TypeFactory.CreateType<PlayerStatus>(reader.ReadUShort());
		Status.Deserialize(reader);
	}
}
