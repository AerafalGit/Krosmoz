// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Game.Context.Roleplay;

namespace Krosmoz.Protocol.Messages.Game.Atlas;

public sealed class AtlasPointInformationsMessage : DofusMessage
{
	public new const uint StaticProtocolId = 5956;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static AtlasPointInformationsMessage Empty =>
		new() { Type = AtlasPointsInformations.Empty };

	public required AtlasPointsInformations Type { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		Type.Serialize(writer);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		Type = AtlasPointsInformations.Empty;
		Type.Deserialize(reader);
	}
}
