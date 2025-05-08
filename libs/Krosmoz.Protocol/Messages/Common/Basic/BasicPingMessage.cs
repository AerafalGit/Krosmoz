// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Common.Basic;

public sealed class BasicPingMessage : DofusMessage
{
	public new const uint StaticProtocolId = 182;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static BasicPingMessage Empty =>
		new() { Quiet = false };

	public required bool Quiet { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteBoolean(Quiet);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		Quiet = reader.ReadBoolean();
	}
}
