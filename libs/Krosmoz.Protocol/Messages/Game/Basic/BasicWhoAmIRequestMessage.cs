// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Basic;

public sealed class BasicWhoAmIRequestMessage : DofusMessage
{
	public new const uint StaticProtocolId = 5664;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static BasicWhoAmIRequestMessage Empty =>
		new() { Verbose = false };

	public required bool Verbose { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteBoolean(Verbose);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		Verbose = reader.ReadBoolean();
	}
}
