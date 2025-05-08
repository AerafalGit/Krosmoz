// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Character.Choice;

public sealed class CharacterFirstSelectionMessage : CharacterSelectionMessage
{
	public new const uint StaticProtocolId = 6084;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static new CharacterFirstSelectionMessage Empty =>
		new() { Id = 0, DoTutorial = false };

	public required bool DoTutorial { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		writer.WriteBoolean(DoTutorial);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		DoTutorial = reader.ReadBoolean();
	}
}
