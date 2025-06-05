// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Context;

public sealed class GameContextKickMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6081;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static GameContextKickMessage Empty =>
		new() { TargetId = 0 };

	public required int TargetId { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteInt32(TargetId);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		TargetId = reader.ReadInt32();
	}
}
