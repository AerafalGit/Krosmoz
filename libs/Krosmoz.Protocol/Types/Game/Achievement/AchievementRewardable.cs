// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Types.Game.Achievement;

public sealed class AchievementRewardable : DofusType
{
	public new const ushort StaticProtocolId = 412;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static AchievementRewardable Empty =>
		new() { Id = 0, Finishedlevel = 0 };

	public required ushort Id { get; set; }

	public required byte Finishedlevel { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteVarUInt16(Id);
		writer.WriteUInt8(Finishedlevel);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		Id = reader.ReadVarUInt16();
		Finishedlevel = reader.ReadUInt8();
	}
}
