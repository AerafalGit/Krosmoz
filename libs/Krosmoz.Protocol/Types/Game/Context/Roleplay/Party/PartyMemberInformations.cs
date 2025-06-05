// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Game.Character.Choice;
using Krosmoz.Protocol.Types.Game.Character.Status;
using Krosmoz.Protocol.Types.Game.Context.Roleplay.Party.Companion;
using Krosmoz.Protocol.Types.Game.Look;

namespace Krosmoz.Protocol.Types.Game.Context.Roleplay.Party;

public class PartyMemberInformations : CharacterBaseInformations
{
	public new const ushort StaticProtocolId = 90;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static new PartyMemberInformations Empty =>
		new() { Id = 0, Name = string.Empty, Level = 0, EntityLook = EntityLook.Empty, Sex = false, Breed = 0, LifePoints = 0, MaxLifePoints = 0, Prospecting = 0, RegenRate = 0, Initiative = 0, AlignmentSide = 0, WorldX = 0, WorldY = 0, MapId = 0, SubAreaId = 0, Status = PlayerStatus.Empty, Companions = [] };

	public required uint LifePoints { get; set; }

	public required uint MaxLifePoints { get; set; }

	public required ushort Prospecting { get; set; }

	public required byte RegenRate { get; set; }

	public required ushort Initiative { get; set; }

	public required sbyte AlignmentSide { get; set; }

	public required short WorldX { get; set; }

	public required short WorldY { get; set; }

	public required int MapId { get; set; }

	public required ushort SubAreaId { get; set; }

	public required PlayerStatus Status { get; set; }

	public required IEnumerable<PartyCompanionMemberInformations> Companions { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		writer.WriteVarUInt32(LifePoints);
		writer.WriteVarUInt32(MaxLifePoints);
		writer.WriteVarUInt16(Prospecting);
		writer.WriteUInt8(RegenRate);
		writer.WriteVarUInt16(Initiative);
		writer.WriteInt8(AlignmentSide);
		writer.WriteInt16(WorldX);
		writer.WriteInt16(WorldY);
		writer.WriteInt32(MapId);
		writer.WriteVarUInt16(SubAreaId);
		writer.WriteUInt16(Status.ProtocolId);
		Status.Serialize(writer);
		var companionsBefore = writer.Position;
		var companionsCount = 0;
		writer.WriteInt16(0);
		foreach (var item in Companions)
		{
			item.Serialize(writer);
			companionsCount++;
		}
		var companionsAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, companionsBefore);
		writer.WriteInt16((short)companionsCount);
		writer.Seek(SeekOrigin.Begin, companionsAfter);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		LifePoints = reader.ReadVarUInt32();
		MaxLifePoints = reader.ReadVarUInt32();
		Prospecting = reader.ReadVarUInt16();
		RegenRate = reader.ReadUInt8();
		Initiative = reader.ReadVarUInt16();
		AlignmentSide = reader.ReadInt8();
		WorldX = reader.ReadInt16();
		WorldY = reader.ReadInt16();
		MapId = reader.ReadInt32();
		SubAreaId = reader.ReadVarUInt16();
		Status = Types.TypeFactory.CreateType<PlayerStatus>(reader.ReadUInt16());
		Status.Deserialize(reader);
		var companionsCount = reader.ReadInt16();
		var companions = new PartyCompanionMemberInformations[companionsCount];
		for (var i = 0; i < companionsCount; i++)
		{
			var entry = PartyCompanionMemberInformations.Empty;
			entry.Deserialize(reader);
			companions[i] = entry;
		}
		Companions = companions;
	}
}
