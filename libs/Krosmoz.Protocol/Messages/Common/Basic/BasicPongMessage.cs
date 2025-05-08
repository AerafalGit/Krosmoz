// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Common.Basic;

public sealed class BasicPongMessage : DofusMessage
{
	public new const uint StaticProtocolId = 183;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static BasicPongMessage Empty =>
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
