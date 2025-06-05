// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Game.Actions.Fight;

namespace Krosmoz.Protocol.Types.Game.Action.Fight;

public sealed class FightDispellableEffectExtendedInformations : DofusType
{
	public new const ushort StaticProtocolId = 208;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static FightDispellableEffectExtendedInformations Empty =>
		new() { ActionId = 0, SourceId = 0, Effect = AbstractFightDispellableEffect.Empty };

	public required ushort ActionId { get; set; }

	public required int SourceId { get; set; }

	public required AbstractFightDispellableEffect Effect { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteVarUInt16(ActionId);
		writer.WriteInt32(SourceId);
		writer.WriteUInt16(Effect.ProtocolId);
		Effect.Serialize(writer);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		ActionId = reader.ReadVarUInt16();
		SourceId = reader.ReadInt32();
		Effect = Types.TypeFactory.CreateType<AbstractFightDispellableEffect>(reader.ReadUInt16());
		Effect.Deserialize(reader);
	}
}
