// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Idol;

public sealed class IdolPartyRegisterRequestMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6582;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static IdolPartyRegisterRequestMessage Empty =>
		new() { Register = false };

	public required bool Register { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteBoolean(Register);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		Register = reader.ReadBoolean();
	}
}
