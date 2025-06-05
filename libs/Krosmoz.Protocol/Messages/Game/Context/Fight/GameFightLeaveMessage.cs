// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Context.Fight;

public sealed class GameFightLeaveMessage : DofusMessage
{
	public new const uint StaticProtocolId = 721;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static GameFightLeaveMessage Empty =>
		new() { CharId = 0 };

	public required int CharId { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteInt32(CharId);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		CharId = reader.ReadInt32();
	}
}
