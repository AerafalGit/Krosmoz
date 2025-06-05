// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Guild;

public sealed class GuildLevelUpMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6062;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static GuildLevelUpMessage Empty =>
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
