// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Web.Krosmaster;

public sealed class KrosmasterPlayingStatusMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6347;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static KrosmasterPlayingStatusMessage Empty =>
		new() { Playing = false };

	public required bool Playing { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteBoolean(Playing);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		Playing = reader.ReadBoolean();
	}
}
