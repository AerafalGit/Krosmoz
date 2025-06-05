// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Game.Look;

namespace Krosmoz.Protocol.Types.Game.Character;

public sealed class CharacterMinimalPlusLookAndGradeInformations : CharacterMinimalPlusLookInformations
{
	public new const ushort StaticProtocolId = 193;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static new CharacterMinimalPlusLookAndGradeInformations Empty =>
		new() { Id = 0, Name = string.Empty, Level = 0, EntityLook = EntityLook.Empty, Grade = 0 };

	public required uint Grade { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		writer.WriteVarUInt32(Grade);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		Grade = reader.ReadVarUInt32();
	}
}
