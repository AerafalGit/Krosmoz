// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Tinsel;

public sealed class TitleLostMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6371;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static TitleLostMessage Empty =>
		new() { TitleId = 0 };

	public required short TitleId { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteInt16(TitleId);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		TitleId = reader.ReadInt16();
	}
}
