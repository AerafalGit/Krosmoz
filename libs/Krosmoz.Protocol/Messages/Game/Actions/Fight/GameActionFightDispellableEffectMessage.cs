// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Game.Actions.Fight;

namespace Krosmoz.Protocol.Messages.Game.Actions.Fight;

public sealed class GameActionFightDispellableEffectMessage : AbstractGameActionMessage
{
	public new const uint StaticProtocolId = 6070;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static new GameActionFightDispellableEffectMessage Empty =>
		new() { SourceId = 0, ActionId = 0, Effect = AbstractFightDispellableEffect.Empty };

	public required AbstractFightDispellableEffect Effect { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		writer.WriteUInt16(Effect.ProtocolId);
		Effect.Serialize(writer);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		Effect = Types.TypeFactory.CreateType<AbstractFightDispellableEffect>(reader.ReadUInt16());
		Effect.Deserialize(reader);
	}
}
