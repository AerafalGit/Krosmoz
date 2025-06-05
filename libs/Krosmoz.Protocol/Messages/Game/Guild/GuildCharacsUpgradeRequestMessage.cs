// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Guild;

public sealed class GuildCharacsUpgradeRequestMessage : DofusMessage
{
	public new const uint StaticProtocolId = 5706;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static GuildCharacsUpgradeRequestMessage Empty =>
		new() { CharaTypeTarget = 0 };

	public required sbyte CharaTypeTarget { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteInt8(CharaTypeTarget);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		CharaTypeTarget = reader.ReadInt8();
	}
}
