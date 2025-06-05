// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Prism;

public sealed class PrismsListRegisterMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6441;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static PrismsListRegisterMessage Empty =>
		new() { Listen = 0 };

	public required sbyte Listen { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteInt8(Listen);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		Listen = reader.ReadInt8();
	}
}
