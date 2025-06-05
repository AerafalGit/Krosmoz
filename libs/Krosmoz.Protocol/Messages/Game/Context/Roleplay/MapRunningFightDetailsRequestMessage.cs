// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Context.Roleplay;

public sealed class MapRunningFightDetailsRequestMessage : DofusMessage
{
	public new const uint StaticProtocolId = 5750;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static MapRunningFightDetailsRequestMessage Empty =>
		new() { FightId = 0 };

	public required int FightId { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteInt32(FightId);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		FightId = reader.ReadInt32();
	}
}
