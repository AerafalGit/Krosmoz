// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Actions.Fight;

public sealed class GameActionFightCloseCombatMessage : AbstractGameActionFightTargetedAbilityMessage
{
	public new const uint StaticProtocolId = 6116;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static new GameActionFightCloseCombatMessage Empty =>
		new() { SourceId = 0, ActionId = 0, SilentCast = false, Critical = 0, DestinationCellId = 0, TargetId = 0, WeaponGenericId = 0 };

	public required ushort WeaponGenericId { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		writer.WriteVarUInt16(WeaponGenericId);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		WeaponGenericId = reader.ReadVarUInt16();
	}
}
