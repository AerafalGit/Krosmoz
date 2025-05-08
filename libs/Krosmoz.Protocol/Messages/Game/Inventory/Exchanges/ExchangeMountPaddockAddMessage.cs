// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Game.Mount;

namespace Krosmoz.Protocol.Messages.Game.Inventory.Exchanges;

public sealed class ExchangeMountPaddockAddMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6049;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static ExchangeMountPaddockAddMessage Empty =>
		new() { MountDescription = MountClientData.Empty };

	public required MountClientData MountDescription { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		MountDescription.Serialize(writer);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		MountDescription = MountClientData.Empty;
		MountDescription.Deserialize(reader);
	}
}
