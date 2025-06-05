// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Game.Look;

namespace Krosmoz.Protocol.Types.Game.Context.Roleplay;

public class GameRolePlayNamedActorInformations : GameRolePlayActorInformations
{
	public new const ushort StaticProtocolId = 154;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static new GameRolePlayNamedActorInformations Empty =>
		new() { Disposition = EntityDispositionInformations.Empty, Look = EntityLook.Empty, ContextualId = 0, Name = string.Empty };

	public required string Name { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		writer.WriteUtfPrefixedLength16(Name);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		Name = reader.ReadUtfPrefixedLength16();
	}
}
