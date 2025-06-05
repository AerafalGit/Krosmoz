// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Tinsel;

public sealed class OrnamentGainedMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6368;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static OrnamentGainedMessage Empty =>
		new() { OrnamentId = 0 };

	public required short OrnamentId { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteInt16(OrnamentId);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		OrnamentId = reader.ReadInt16();
	}
}
