// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Types.Game.Context.Roleplay;

public class MonsterInGroupLightInformations : DofusType
{
	public new const ushort StaticProtocolId = 395;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static MonsterInGroupLightInformations Empty =>
		new() { CreatureGenericId = 0, Grade = 0 };

	public required int CreatureGenericId { get; set; }

	public required sbyte Grade { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteInt32(CreatureGenericId);
		writer.WriteInt8(Grade);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		CreatureGenericId = reader.ReadInt32();
		Grade = reader.ReadInt8();
	}
}
