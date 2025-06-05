// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Context.Roleplay.Document;

public sealed class DocumentReadingBeginMessage : DofusMessage
{
	public new const uint StaticProtocolId = 5675;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static DocumentReadingBeginMessage Empty =>
		new() { DocumentId = 0 };

	public required ushort DocumentId { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteVarUInt16(DocumentId);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		DocumentId = reader.ReadVarUInt16();
	}
}
