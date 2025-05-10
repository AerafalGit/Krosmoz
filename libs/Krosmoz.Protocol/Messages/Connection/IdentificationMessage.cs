// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Version;

namespace Krosmoz.Protocol.Messages.Connection;

public class IdentificationMessage : DofusMessage
{
	public new const uint StaticProtocolId = 4;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static IdentificationMessage Empty =>
		new() { Autoconnect = false, UseCertificate = false, UseLoginToken = false, Version = VersionExtended.Empty, Lang = string.Empty, Username = string.Empty, Password = string.Empty, ServerId = 0, SessionOptionalSalt = 0 };

	public required bool Autoconnect { get; set; }

	public required bool UseCertificate { get; set; }

	public required bool UseLoginToken { get; set; }

	public required VersionExtended Version { get; set; }

	public required string Lang { get; set; }

	public required string Username { get; set; }

    public required string Password { get; set; }

	public required short ServerId { get; set; }

	public required double SessionOptionalSalt { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		var flag = new byte();
		flag = BooleanByteWrapper.SetFlag(flag, 0, Autoconnect);
		flag = BooleanByteWrapper.SetFlag(flag, 1, UseCertificate);
		flag = BooleanByteWrapper.SetFlag(flag, 2, UseLoginToken);
		writer.WriteByte(flag);
		Version.Serialize(writer);
		writer.WriteUtfLengthPrefixed16(Lang);
		writer.WriteUtfLengthPrefixed16(Username);
        writer.WriteUtfLengthPrefixed16(Password);
		writer.WriteShort(ServerId);
		writer.WriteDouble(SessionOptionalSalt);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		var flag = reader.ReadByte();
		Autoconnect = BooleanByteWrapper.GetFlag(flag, 0);
		UseCertificate = BooleanByteWrapper.GetFlag(flag, 1);
		UseLoginToken = BooleanByteWrapper.GetFlag(flag, 2);
		Version = VersionExtended.Empty;
		Version.Deserialize(reader);
		Lang = reader.ReadUtfLengthPrefixed16();
		Username = reader.ReadUtfLengthPrefixed16();
        Password = reader.ReadUtfLengthPrefixed16();
		ServerId = reader.ReadShort();
		SessionOptionalSalt = reader.ReadDouble();
	}
}
