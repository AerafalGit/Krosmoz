// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Game.Look;

namespace Krosmoz.Protocol.Types.Game.Character;

public class CharacterMinimalPlusLookInformations : CharacterMinimalInformations
{
	public new const ushort StaticProtocolId = 163;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static new CharacterMinimalPlusLookInformations Empty =>
		new() { Id = 0, Name = string.Empty, Level = 0, EntityLook = EntityLook.Empty };

	public required EntityLook EntityLook { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		EntityLook.Serialize(writer);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		EntityLook = EntityLook.Empty;
		EntityLook.Deserialize(reader);
	}
}
