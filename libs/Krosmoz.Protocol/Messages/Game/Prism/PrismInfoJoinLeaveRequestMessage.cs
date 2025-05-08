// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Prism;

public sealed class PrismInfoJoinLeaveRequestMessage : DofusMessage
{
	public new const uint StaticProtocolId = 5844;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static PrismInfoJoinLeaveRequestMessage Empty =>
		new() { Join = false };

	public required bool Join { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteBoolean(Join);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		Join = reader.ReadBoolean();
	}
}
