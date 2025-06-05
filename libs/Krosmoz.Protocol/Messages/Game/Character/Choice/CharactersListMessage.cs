// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Character.Choice;

public class CharactersListMessage : BasicCharactersListMessage
{
	public new const uint StaticProtocolId = 151;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static new CharactersListMessage Empty =>
		new() { Characters = [], HasStartupActions = false };

	public required bool HasStartupActions { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		writer.WriteBoolean(HasStartupActions);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		HasStartupActions = reader.ReadBoolean();
	}
}
