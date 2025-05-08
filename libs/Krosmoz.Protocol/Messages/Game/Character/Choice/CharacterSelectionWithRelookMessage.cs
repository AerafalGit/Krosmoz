// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Character.Choice;

public sealed class CharacterSelectionWithRelookMessage : CharacterSelectionMessage
{
	public new const uint StaticProtocolId = 6353;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static new CharacterSelectionWithRelookMessage Empty =>
		new() { Id = 0, CosmeticId = 0 };

	public required int CosmeticId { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		writer.WriteInt(CosmeticId);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		CosmeticId = reader.ReadInt();
	}
}
