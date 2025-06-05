// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Game.Character.Choice;
using Krosmoz.Protocol.Types.Game.Context.Roleplay.Party.Companion;
using Krosmoz.Protocol.Types.Game.Look;

namespace Krosmoz.Protocol.Types.Game.Context.Roleplay.Party;

public sealed class PartyInvitationMemberInformations : CharacterBaseInformations
{
	public new const ushort StaticProtocolId = 376;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static new PartyInvitationMemberInformations Empty =>
		new() { Id = 0, Name = string.Empty, Level = 0, EntityLook = EntityLook.Empty, Sex = false, Breed = 0, WorldX = 0, WorldY = 0, MapId = 0, SubAreaId = 0, Companions = [] };

	public required short WorldX { get; set; }

	public required short WorldY { get; set; }

	public required int MapId { get; set; }

	public required ushort SubAreaId { get; set; }

	public required IEnumerable<PartyCompanionBaseInformations> Companions { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		writer.WriteInt16(WorldX);
		writer.WriteInt16(WorldY);
		writer.WriteInt32(MapId);
		writer.WriteVarUInt16(SubAreaId);
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
		WorldX = reader.ReadInt16();
		WorldY = reader.ReadInt16();
		MapId = reader.ReadInt32();
		SubAreaId = reader.ReadVarUInt16();
		var companionsCount = reader.ReadInt16();
		var companions = new PartyCompanionBaseInformations[companionsCount];
		for (var i = 0; i < companionsCount; i++)
		{
			var entry = PartyCompanionBaseInformations.Empty;
			entry.Deserialize(reader);
			companions[i] = entry;
		}
		Companions = companions;
	}
}
