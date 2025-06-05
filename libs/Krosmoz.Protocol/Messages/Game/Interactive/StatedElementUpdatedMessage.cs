// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Game.Interactive;

namespace Krosmoz.Protocol.Messages.Game.Interactive;

public sealed class StatedElementUpdatedMessage : DofusMessage
{
	public new const uint StaticProtocolId = 5709;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static StatedElementUpdatedMessage Empty =>
		new() { StatedElement = StatedElement.Empty };

	public required StatedElement StatedElement { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		StatedElement.Serialize(writer);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		StatedElement = StatedElement.Empty;
		StatedElement.Deserialize(reader);
	}
}
