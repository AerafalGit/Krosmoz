// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Script;

public sealed class URLOpenMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6266;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static URLOpenMessage Empty =>
		new() { UrlId = 0 };

	public required sbyte UrlId { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteInt8(UrlId);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		UrlId = reader.ReadInt8();
	}
}
