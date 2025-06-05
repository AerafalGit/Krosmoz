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
		new() { Trusted = false, Certified = false };

	public required bool Trusted { get; set; }

	public required bool Certified { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		var flag = new byte();
		flag = BooleanByteWrapper.SetFlag(flag, 0, Trusted);
		flag = BooleanByteWrapper.SetFlag(flag, 1, Certified);
		writer.WriteUInt8(flag);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		var flag = reader.ReadUInt8();
		Trusted = BooleanByteWrapper.GetFlag(flag, 0);
		Certified = BooleanByteWrapper.GetFlag(flag, 1);
	}
}
