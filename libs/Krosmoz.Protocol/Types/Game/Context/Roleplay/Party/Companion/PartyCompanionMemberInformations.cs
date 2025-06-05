// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Game.Look;

namespace Krosmoz.Protocol.Types.Game.Context.Roleplay.Party.Companion;

public sealed class PartyCompanionMemberInformations : PartyCompanionBaseInformations
{
	public new const ushort StaticProtocolId = 452;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static new PartyCompanionMemberInformations Empty =>
		new() { EntityLook = EntityLook.Empty, CompanionGenericId = 0, IndexId = 0, Initiative = 0, LifePoints = 0, MaxLifePoints = 0, Prospecting = 0, RegenRate = 0 };

	public required ushort Initiative { get; set; }

	public required uint LifePoints { get; set; }

	public required uint MaxLifePoints { get; set; }

	public required ushort Prospecting { get; set; }

	public required byte RegenRate { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		writer.WriteVarUInt16(Initiative);
		writer.WriteVarUInt32(LifePoints);
		writer.WriteVarUInt32(MaxLifePoints);
		writer.WriteVarUInt16(Prospecting);
		writer.WriteUInt8(RegenRate);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		Initiative = reader.ReadVarUInt16();
		LifePoints = reader.ReadVarUInt32();
		MaxLifePoints = reader.ReadVarUInt32();
		Prospecting = reader.ReadVarUInt16();
		RegenRate = reader.ReadUInt8();
	}
}
