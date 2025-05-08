// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Initialization;

public sealed class CharacterCapabilitiesMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6339;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static CharacterCapabilitiesMessage Empty =>
		new() { GuildEmblemSymbolCategories = 0 };

	public required int GuildEmblemSymbolCategories { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteInt(GuildEmblemSymbolCategories);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		GuildEmblemSymbolCategories = reader.ReadInt();
	}
}
