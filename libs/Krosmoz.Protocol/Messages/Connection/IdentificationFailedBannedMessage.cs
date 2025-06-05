// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Connection;

public sealed class IdentificationFailedBannedMessage : IdentificationFailedMessage
{
	public new const uint StaticProtocolId = 6174;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static new IdentificationFailedBannedMessage Empty =>
		new() { Reason = 0, BanEndDate = 0 };

	public required double BanEndDate { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		writer.WriteDouble(BanEndDate);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		BanEndDate = reader.ReadDouble();
	}
}
