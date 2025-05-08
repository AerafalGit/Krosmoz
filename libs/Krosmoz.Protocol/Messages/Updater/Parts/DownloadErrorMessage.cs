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
		writer.WriteSByte(ErrorId);
		writer.WriteUtfLengthPrefixed16(Message);
		writer.WriteUtfLengthPrefixed16(HelpUrl);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		ErrorId = reader.ReadSByte();
		Message = reader.ReadUtfLengthPrefixed16();
		HelpUrl = reader.ReadUtfLengthPrefixed16();
	}
}
