// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Game.Look;

namespace Krosmoz.Protocol.Types.Game.Character.Choice;

public sealed class CharacterHardcoreOrEpicInformations : CharacterBaseInformations
{
	public new const ushort StaticProtocolId = 474;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static new CharacterHardcoreOrEpicInformations Empty =>
		new() { Id = 0, Name = string.Empty, Level = 0, EntityLook = EntityLook.Empty, Sex = false, Breed = 0, DeathState = 0, DeathCount = 0, DeathMaxLevel = 0 };

	public required sbyte DeathState { get; set; }

	public required ushort DeathCount { get; set; }

	public required byte DeathMaxLevel { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		writer.WriteInt8(DeathState);
		writer.WriteVarUInt16(DeathCount);
		writer.WriteUInt8(DeathMaxLevel);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		DeathState = reader.ReadInt8();
		DeathCount = reader.ReadVarUInt16();
		DeathMaxLevel = reader.ReadUInt8();
	}
}
