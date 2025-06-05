// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Updater.Parts;

public sealed class DownloadErrorMessage : DofusMessage
{
	public new const uint StaticProtocolId = 1513;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static DownloadErrorMessage Empty =>
		new() { ErrorId = 0, Message = string.Empty, HelpUrl = string.Empty };

	public required sbyte ErrorId { get; set; }

	public required string Message { get; set; }

	public required string HelpUrl { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteInt8(ErrorId);
		writer.WriteUtfPrefixedLength16(Message);
		writer.WriteUtfPrefixedLength16(HelpUrl);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		ErrorId = reader.ReadInt8();
		Message = reader.ReadUtfPrefixedLength16();
		HelpUrl = reader.ReadUtfPrefixedLength16();
	}
}
