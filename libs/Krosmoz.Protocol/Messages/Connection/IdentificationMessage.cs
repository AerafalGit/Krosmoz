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
		new() { Autoconnect = false, UseCertificate = false, UseLoginToken = false, Version = VersionExtended.Empty, Lang = string.Empty, Credentials = [], ServerId = 0, SessionOptionalSalt = 0 };

	public required bool Autoconnect { get; set; }

	public required bool UseCertificate { get; set; }

	public required bool UseLoginToken { get; set; }

	public required VersionExtended Version { get; set; }

	public required string Lang { get; set; }

	public required IEnumerable<sbyte> Credentials { get; set; }

	public required short ServerId { get; set; }

	public required double SessionOptionalSalt { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		var flag = new byte();
		BooleanByteWrapper.SetFlag(flag, 0, Autoconnect);
		BooleanByteWrapper.SetFlag(flag, 1, UseCertificate);
		BooleanByteWrapper.SetFlag(flag, 2, UseLoginToken);
		writer.WriteByte(flag);
		Version.Serialize(writer);
		writer.WriteUtfLengthPrefixed16(Lang);
		var credentialsBefore = writer.Position;
		var credentialsCount = 0;
		writer.WriteShort(0);
		foreach (var item in Credentials)
		{
			writer.WriteSByte(item);
			credentialsCount++;
		}
		var credentialsAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, credentialsBefore);
		writer.WriteShort((short)credentialsCount);
		writer.Seek(SeekOrigin.Begin, credentialsAfter);
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
		var credentialsCount = reader.ReadShort();
		var credentials = new sbyte[credentialsCount];
		for (var i = 0; i < credentialsCount; i++)
		{
			credentials[i] = reader.ReadSByte();
		}
		Credentials = credentials;
		ServerId = reader.ReadShort();
		SessionOptionalSalt = reader.ReadDouble();
	}
}
