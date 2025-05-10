// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Version;

namespace Krosmoz.Protocol.Messages.Connection;

public sealed class IdentificationAccountForceMessage : IdentificationMessage
{
	public new const uint StaticProtocolId = 6119;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static new IdentificationAccountForceMessage Empty =>
		new() { SessionOptionalSalt = 0, ServerId = 0, Username = string.Empty, Password = string.Empty, Lang = string.Empty, Version = VersionExtended.Empty, UseLoginToken = false, UseCertificate = false, Autoconnect = false, ForcedAccountLogin = string.Empty };

	public required string ForcedAccountLogin { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		writer.WriteUtfLengthPrefixed16(ForcedAccountLogin);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		ForcedAccountLogin = reader.ReadUtfLengthPrefixed16();
	}
}
