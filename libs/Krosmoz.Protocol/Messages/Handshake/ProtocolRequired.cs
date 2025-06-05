// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Handshake;

public sealed class ProtocolRequired : DofusMessage
{
	public new const uint StaticProtocolId = 1;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static ProtocolRequired Empty =>
		new() { RequiredVersion = 0, CurrentVersion = 0 };

	public required int RequiredVersion { get; set; }

	public required int CurrentVersion { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteInt32(RequiredVersion);
		writer.WriteInt32(CurrentVersion);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		RequiredVersion = reader.ReadInt32();
		CurrentVersion = reader.ReadInt32();
	}
}
