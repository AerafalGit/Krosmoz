// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Types.Game.Data.Items.Effects;

public class ObjectEffectCreature : ObjectEffect
{
	public new const ushort StaticProtocolId = 71;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static new ObjectEffectCreature Empty =>
		new() { ActionId = 0, MonsterFamilyId = 0 };

	public required ushort MonsterFamilyId { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		writer.WriteVarUInt16(MonsterFamilyId);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		MonsterFamilyId = reader.ReadVarUInt16();
	}
}
