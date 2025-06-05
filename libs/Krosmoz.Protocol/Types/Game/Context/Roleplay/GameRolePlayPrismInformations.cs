// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Game.Look;
using Krosmoz.Protocol.Types.Game.Prism;

namespace Krosmoz.Protocol.Types.Game.Context.Roleplay;

public sealed class GameRolePlayPrismInformations : GameRolePlayActorInformations
{
	public new const ushort StaticProtocolId = 161;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static new GameRolePlayPrismInformations Empty =>
		new() { Disposition = EntityDispositionInformations.Empty, Look = EntityLook.Empty, ContextualId = 0, Prism = PrismInformation.Empty };

	public required PrismInformation Prism { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		writer.WriteUInt16(Prism.ProtocolId);
		Prism.Serialize(writer);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		Prism = Types.TypeFactory.CreateType<PrismInformation>(reader.ReadUInt16());
		Prism.Deserialize(reader);
	}
}
