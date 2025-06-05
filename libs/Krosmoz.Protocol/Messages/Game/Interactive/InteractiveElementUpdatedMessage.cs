// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Game.Interactive;

namespace Krosmoz.Protocol.Messages.Game.Interactive;

public sealed class InteractiveElementUpdatedMessage : DofusMessage
{
	public new const uint StaticProtocolId = 5708;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static InteractiveElementUpdatedMessage Empty =>
		new() { InteractiveElement = InteractiveElement.Empty };

	public required InteractiveElement InteractiveElement { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		InteractiveElement.Serialize(writer);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		InteractiveElement = InteractiveElement.Empty;
		InteractiveElement.Deserialize(reader);
	}
}
