// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Context.Roleplay.Document;

public sealed class ComicReadingBeginMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6536;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static ComicReadingBeginMessage Empty =>
		new() { ComicId = 0 };

	public required ushort ComicId { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteVarUInt16(ComicId);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		ComicId = reader.ReadVarUInt16();
	}
}
