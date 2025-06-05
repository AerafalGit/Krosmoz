// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Types.Secure;

public sealed class TrustCertificate : DofusType
{
	public new const ushort StaticProtocolId = 377;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static TrustCertificate Empty =>
		new() { Id = 0, Hash = string.Empty };

	public required int Id { get; set; }

	public required string Hash { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteInt32(Id);
		writer.WriteUtfPrefixedLength16(Hash);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		Id = reader.ReadInt32();
		Hash = reader.ReadUtfPrefixedLength16();
	}
}
