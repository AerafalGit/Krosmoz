// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Types.Game.Character.Choice;

public sealed class CharacterToRemodelInformations : CharacterRemodelingInformation
{
	public new const ushort StaticProtocolId = 477;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static new CharacterToRemodelInformations Empty =>
		new() { Id = 0, Colors = [], CosmeticId = 0, Sex = false, Breed = 0, Name = string.Empty, PossibleChangeMask = 0, MandatoryChangeMask = 0 };

	public required sbyte PossibleChangeMask { get; set; }

	public required sbyte MandatoryChangeMask { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		writer.WriteInt8(PossibleChangeMask);
		writer.WriteInt8(MandatoryChangeMask);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		PossibleChangeMask = reader.ReadInt8();
		MandatoryChangeMask = reader.ReadInt8();
	}
}
