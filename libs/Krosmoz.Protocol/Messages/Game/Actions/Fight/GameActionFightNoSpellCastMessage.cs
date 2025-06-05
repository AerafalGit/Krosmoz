// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Actions.Fight;

public sealed class GameActionFightNoSpellCastMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6132;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static GameActionFightNoSpellCastMessage Empty =>
		new() { SpellLevelId = 0 };

	public required uint SpellLevelId { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteVarUInt32(SpellLevelId);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		SpellLevelId = reader.ReadVarUInt32();
	}
}
