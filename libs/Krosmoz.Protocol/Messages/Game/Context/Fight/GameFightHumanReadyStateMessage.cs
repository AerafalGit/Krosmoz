// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Context.Fight;

public sealed class GameFightHumanReadyStateMessage : DofusMessage
{
	public new const uint StaticProtocolId = 740;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static GameFightHumanReadyStateMessage Empty =>
		new() { CharacterId = 0, IsReady = false };

	public required uint CharacterId { get; set; }

	public required bool IsReady { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteVarUInt32(CharacterId);
		writer.WriteBoolean(IsReady);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		CharacterId = reader.ReadVarUInt32();
		IsReady = reader.ReadBoolean();
	}
}
