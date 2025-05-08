// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Game.Context;

namespace Krosmoz.Protocol.Messages.Game.Context;

public sealed class GameMapChangeOrientationMessage : DofusMessage
{
	public new const uint StaticProtocolId = 946;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static GameMapChangeOrientationMessage Empty =>
		new() { Orientation = ActorOrientation.Empty };

	public required ActorOrientation Orientation { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		Orientation.Serialize(writer);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		Orientation = ActorOrientation.Empty;
		Orientation.Deserialize(reader);
	}
}
