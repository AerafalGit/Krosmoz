// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Game.Look;

namespace Krosmoz.Protocol.Types.Game.Context;

public class GameContextActorInformations : DofusType
{
	public new const ushort StaticProtocolId = 150;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static GameContextActorInformations Empty =>
		new() { ContextualId = 0, Look = EntityLook.Empty, Disposition = EntityDispositionInformations.Empty };

	public required int ContextualId { get; set; }

	public required EntityLook Look { get; set; }

	public required EntityDispositionInformations Disposition { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteInt32(ContextualId);
		Look.Serialize(writer);
		writer.WriteUInt16(Disposition.ProtocolId);
		Disposition.Serialize(writer);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		ContextualId = reader.ReadInt32();
		Look = EntityLook.Empty;
		Look.Deserialize(reader);
		Disposition = Types.TypeFactory.CreateType<EntityDispositionInformations>(reader.ReadUInt16());
		Disposition.Deserialize(reader);
	}
}
