// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Context.Mount;

public sealed class MountSetXpRatioRequestMessage : DofusMessage
{
	public new const uint StaticProtocolId = 5989;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static MountSetXpRatioRequestMessage Empty =>
		new() { XpRatio = 0 };

	public required sbyte XpRatio { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteInt8(XpRatio);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		XpRatio = reader.ReadInt8();
	}
}
