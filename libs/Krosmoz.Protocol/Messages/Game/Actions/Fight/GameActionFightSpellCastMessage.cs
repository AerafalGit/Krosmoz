// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Actions.Fight;

public sealed class GameActionFightSpellCastMessage : AbstractGameActionFightTargetedAbilityMessage
{
	public new const uint StaticProtocolId = 1010;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static new GameActionFightSpellCastMessage Empty =>
		new() { SourceId = 0, ActionId = 0, SilentCast = false, Critical = 0, DestinationCellId = 0, TargetId = 0, SpellId = 0, SpellLevel = 0, PortalsIds = [] };

	public required ushort SpellId { get; set; }

	public required sbyte SpellLevel { get; set; }

	public required IEnumerable<short> PortalsIds { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		writer.WriteVarUInt16(SpellId);
		writer.WriteInt8(SpellLevel);
		var portalsIdsBefore = writer.Position;
		var portalsIdsCount = 0;
		writer.WriteInt16(0);
		foreach (var item in PortalsIds)
		{
			writer.WriteInt16(item);
			portalsIdsCount++;
		}
		var portalsIdsAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, portalsIdsBefore);
		writer.WriteInt16((short)portalsIdsCount);
		writer.Seek(SeekOrigin.Begin, portalsIdsAfter);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		SpellId = reader.ReadVarUInt16();
		SpellLevel = reader.ReadInt8();
		var portalsIdsCount = reader.ReadInt16();
		var portalsIds = new short[portalsIdsCount];
		for (var i = 0; i < portalsIdsCount; i++)
		{
			portalsIds[i] = reader.ReadInt16();
		}
		PortalsIds = portalsIds;
	}
}
