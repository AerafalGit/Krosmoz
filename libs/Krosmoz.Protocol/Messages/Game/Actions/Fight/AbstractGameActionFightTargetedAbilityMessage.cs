// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Actions.Fight;

public class AbstractGameActionFightTargetedAbilityMessage : AbstractGameActionMessage
{
	public new const uint StaticProtocolId = 6118;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static new AbstractGameActionFightTargetedAbilityMessage Empty =>
		new() { SourceId = 0, ActionId = 0, TargetId = 0, DestinationCellId = 0, Critical = 0, SilentCast = false };

	public required int TargetId { get; set; }

	public required short DestinationCellId { get; set; }

	public required sbyte Critical { get; set; }

	public required bool SilentCast { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		writer.WriteInt32(TargetId);
		writer.WriteInt16(DestinationCellId);
		writer.WriteInt8(Critical);
		writer.WriteBoolean(SilentCast);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		TargetId = reader.ReadInt32();
		DestinationCellId = reader.ReadInt16();
		Critical = reader.ReadInt8();
		SilentCast = reader.ReadBoolean();
	}
}
