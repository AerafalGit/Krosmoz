// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Tinsel;

public sealed class OrnamentSelectedMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6369;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static OrnamentSelectedMessage Empty =>
		new() { OrnamentId = 0 };

	public required ushort OrnamentId { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteVarUInt16(OrnamentId);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		OrnamentId = reader.ReadVarUInt16();
	}
}
