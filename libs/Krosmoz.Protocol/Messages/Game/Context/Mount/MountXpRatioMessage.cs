// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Context.Mount;

public sealed class MountXpRatioMessage : DofusMessage
{
	public new const uint StaticProtocolId = 5970;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static MountXpRatioMessage Empty =>
		new() { Ratio = 0 };

	public required sbyte Ratio { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteInt8(Ratio);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		Ratio = reader.ReadInt8();
	}
}
