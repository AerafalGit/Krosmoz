// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Game.Mount;

namespace Krosmoz.Protocol.Messages.Game.Context.Mount;

public sealed class MountSetMessage : DofusMessage
{
	public new const uint StaticProtocolId = 5968;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static MountSetMessage Empty =>
		new() { MountData = MountClientData.Empty };

	public required MountClientData MountData { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		MountData.Serialize(writer);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		MountData = MountClientData.Empty;
		MountData.Deserialize(reader);
	}
}
