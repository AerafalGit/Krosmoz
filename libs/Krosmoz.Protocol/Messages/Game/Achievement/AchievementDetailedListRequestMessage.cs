// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Achievement;

public sealed class AchievementDetailedListRequestMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6357;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static AchievementDetailedListRequestMessage Empty =>
		new() { CategoryId = 0 };

	public required ushort CategoryId { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteVarUInt16(CategoryId);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		CategoryId = reader.ReadVarUInt16();
	}
}
