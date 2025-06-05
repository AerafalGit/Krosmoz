// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Updater.Parts;

public sealed class DownloadPartMessage : DofusMessage
{
	public new const uint StaticProtocolId = 1503;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static DownloadPartMessage Empty =>
		new() { Id = string.Empty };

	public required string Id { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteUtfPrefixedLength16(Id);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		Id = reader.ReadUtfPrefixedLength16();
	}
}
