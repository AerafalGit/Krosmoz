// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Web.Krosmaster;

public sealed class KrosmasterTransferRequestMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6349;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static KrosmasterTransferRequestMessage Empty =>
		new() { Uid = string.Empty };

	public required string Uid { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteUtfPrefixedLength16(Uid);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		Uid = reader.ReadUtfPrefixedLength16();
	}
}
