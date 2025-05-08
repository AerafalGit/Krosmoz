// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Secure;

public sealed class TrustStatusMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6267;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static TrustStatusMessage Empty =>
		new() { Trusted = false };

	public required bool Trusted { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteBoolean(Trusted);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		Trusted = reader.ReadBoolean();
	}
}
