// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Approach;

public sealed class ServerSettingsMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6340;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static ServerSettingsMessage Empty =>
		new() { Lang = string.Empty, Community = 0, GameType = 0 };

	public required string Lang { get; set; }

	public required sbyte Community { get; set; }

	public required sbyte GameType { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteUtfPrefixedLength16(Lang);
		writer.WriteInt8(Community);
		writer.WriteInt8(GameType);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		Lang = reader.ReadUtfPrefixedLength16();
		Community = reader.ReadInt8();
		GameType = reader.ReadInt8();
	}
}
