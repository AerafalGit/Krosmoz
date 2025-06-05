// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Character.Stats;

public sealed class LifePointsRegenBeginMessage : DofusMessage
{
	public new const uint StaticProtocolId = 5684;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static LifePointsRegenBeginMessage Empty =>
		new() { RegenRate = 0 };

	public required byte RegenRate { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteUInt8(RegenRate);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		RegenRate = reader.ReadUInt8();
	}
}
