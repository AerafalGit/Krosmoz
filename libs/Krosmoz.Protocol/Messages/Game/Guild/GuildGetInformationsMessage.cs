// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Guild;

public sealed class GuildGetInformationsMessage : DofusMessage
{
	public new const uint StaticProtocolId = 5550;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static GuildGetInformationsMessage Empty =>
		new() { InfoType = 0 };

	public required sbyte InfoType { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteInt8(InfoType);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		InfoType = reader.ReadInt8();
	}
}
