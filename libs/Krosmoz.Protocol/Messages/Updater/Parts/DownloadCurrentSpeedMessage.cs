// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Updater.Parts;

public sealed class DownloadCurrentSpeedMessage : DofusMessage
{
	public new const uint StaticProtocolId = 1511;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static DownloadCurrentSpeedMessage Empty =>
		new() { DownloadSpeed = 0 };

	public required sbyte DownloadSpeed { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteInt8(DownloadSpeed);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		DownloadSpeed = reader.ReadInt8();
	}
}
