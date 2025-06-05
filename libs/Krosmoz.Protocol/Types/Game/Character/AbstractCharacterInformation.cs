// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Types.Game.Character;

public class AbstractCharacterInformation : DofusType
{
	public new const ushort StaticProtocolId = 400;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static AbstractCharacterInformation Empty =>
		new() { Id = 0 };

	public required uint Id { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteVarUInt32(Id);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		Id = reader.ReadVarUInt32();
	}
}
