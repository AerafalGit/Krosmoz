// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Types.Game.Data.Items.Effects;

public sealed class ObjectEffectLadder : ObjectEffectCreature
{
	public new const ushort StaticProtocolId = 81;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static new ObjectEffectLadder Empty =>
		new() { ActionId = 0, MonsterFamilyId = 0, MonsterCount = 0 };

	public required uint MonsterCount { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		writer.WriteVarUInt32(MonsterCount);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		MonsterCount = reader.ReadVarUInt32();
	}
}
