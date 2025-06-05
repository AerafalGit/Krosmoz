// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Game.Look;

namespace Krosmoz.Protocol.Types.Game.Context.Roleplay;

public sealed class GameRolePlayMountInformations : GameRolePlayNamedActorInformations
{
	public new const ushort StaticProtocolId = 180;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static new GameRolePlayMountInformations Empty =>
		new() { Disposition = EntityDispositionInformations.Empty, Look = EntityLook.Empty, ContextualId = 0, Name = string.Empty, OwnerName = string.Empty, Level = 0 };

	public required string OwnerName { get; set; }

	public required byte Level { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		writer.WriteUtfPrefixedLength16(OwnerName);
		writer.WriteUInt8(Level);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		OwnerName = reader.ReadUtfPrefixedLength16();
		Level = reader.ReadUInt8();
	}
}
