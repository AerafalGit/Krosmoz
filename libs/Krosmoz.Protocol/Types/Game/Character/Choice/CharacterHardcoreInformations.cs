// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Game.Look;

namespace Krosmoz.Protocol.Types.Game.Character.Choice;

public sealed class CharacterHardcoreInformations : CharacterBaseInformations
{
	public new const ushort StaticProtocolId = 86;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static new CharacterHardcoreInformations Empty =>
		new() { Id = 0, Name = string.Empty, Level = 0, EntityLook = EntityLook.Empty, Sex = false, Breed = 0, DeathState = 0, DeathCount = 0, DeathMaxLevel = 0 };

	public required sbyte DeathState { get; set; }

	public required short DeathCount { get; set; }

	public required byte DeathMaxLevel { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		writer.WriteSByte(DeathState);
		writer.WriteShort(DeathCount);
		writer.WriteByte(DeathMaxLevel);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		DeathState = reader.ReadSByte();
		DeathCount = reader.ReadShort();
		DeathMaxLevel = reader.ReadByte();
	}
}
