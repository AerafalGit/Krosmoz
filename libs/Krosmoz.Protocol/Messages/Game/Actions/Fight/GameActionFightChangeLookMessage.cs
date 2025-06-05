// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Game.Look;

namespace Krosmoz.Protocol.Messages.Game.Actions.Fight;

public sealed class GameActionFightChangeLookMessage : AbstractGameActionMessage
{
	public new const uint StaticProtocolId = 5532;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static new GameActionFightChangeLookMessage Empty =>
		new() { SourceId = 0, ActionId = 0, TargetId = 0, EntityLook = EntityLook.Empty };

	public required int TargetId { get; set; }

	public required EntityLook EntityLook { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		writer.WriteInt32(TargetId);
		EntityLook.Serialize(writer);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		TargetId = reader.ReadInt32();
		EntityLook = EntityLook.Empty;
		EntityLook.Deserialize(reader);
	}
}
