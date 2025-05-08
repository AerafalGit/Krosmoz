// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Game.Look;

namespace Krosmoz.Protocol.Types.Game.Context.Roleplay;

public sealed class MonsterInGroupInformations : MonsterInGroupLightInformations
{
	public new const ushort StaticProtocolId = 144;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static new MonsterInGroupInformations Empty =>
		new() { Grade = 0, CreatureGenericId = 0, Look = EntityLook.Empty };

	public required EntityLook Look { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		Look.Serialize(writer);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		Look = EntityLook.Empty;
		Look.Deserialize(reader);
	}
}
