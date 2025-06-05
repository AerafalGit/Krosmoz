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

	public required uint GuildEmblemSymbolCategories { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteVarUInt32(GuildEmblemSymbolCategories);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		GuildEmblemSymbolCategories = reader.ReadVarUInt32();
	}
}
