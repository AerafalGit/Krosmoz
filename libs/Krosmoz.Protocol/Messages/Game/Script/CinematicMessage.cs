// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Script;

public sealed class CinematicMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6053;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static CinematicMessage Empty =>
		new() { CinematicId = 0 };

	public required ushort CinematicId { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteVarUInt16(CinematicId);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		CinematicId = reader.ReadVarUInt16();
	}
}
