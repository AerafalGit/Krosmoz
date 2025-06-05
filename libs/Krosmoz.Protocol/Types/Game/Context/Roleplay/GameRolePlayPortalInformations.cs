// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Game.Context.Roleplay.TreasureHunt;
using Krosmoz.Protocol.Types.Game.Look;

namespace Krosmoz.Protocol.Types.Game.Context.Roleplay;

public sealed class GameRolePlayPortalInformations : GameRolePlayActorInformations
{
	public new const ushort StaticProtocolId = 467;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static new GameRolePlayPortalInformations Empty =>
		new() { Disposition = EntityDispositionInformations.Empty, Look = EntityLook.Empty, ContextualId = 0, Portal = PortalInformation.Empty };

	public required PortalInformation Portal { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		writer.WriteUInt16(Portal.ProtocolId);
		Portal.Serialize(writer);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		Portal = Types.TypeFactory.CreateType<PortalInformation>(reader.ReadUInt16());
		Portal.Deserialize(reader);
	}
}
