// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Pvp;

public sealed class SetEnableAVARequestMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6443;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static SetEnableAVARequestMessage Empty =>
		new() { Enable = false };

	public required bool Enable { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteBoolean(Enable);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		Enable = reader.ReadBoolean();
	}
}
