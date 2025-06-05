// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Game.Look;

namespace Krosmoz.Protocol.Types.Game.Character.Choice;

public class CharacterBaseInformations : CharacterMinimalPlusLookInformations
{
	public new const ushort StaticProtocolId = 45;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static new CharacterBaseInformations Empty =>
		new() { Id = 0, Name = string.Empty, Level = 0, EntityLook = EntityLook.Empty, Breed = 0, Sex = false };

	public required sbyte Breed { get; set; }

	public required bool Sex { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		writer.WriteInt8(Breed);
		writer.WriteBoolean(Sex);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		Breed = reader.ReadInt8();
		Sex = reader.ReadBoolean();
	}
}
