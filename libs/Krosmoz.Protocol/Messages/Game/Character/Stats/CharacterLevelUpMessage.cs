// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Character.Stats;

public class CharacterLevelUpMessage : DofusMessage
{
	public new const uint StaticProtocolId = 5670;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static CharacterLevelUpMessage Empty =>
		new() { NewLevel = 0 };

	public required byte NewLevel { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteUInt8(NewLevel);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		NewLevel = reader.ReadUInt8();
	}
}
