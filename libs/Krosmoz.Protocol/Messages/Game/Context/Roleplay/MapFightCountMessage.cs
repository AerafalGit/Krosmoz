// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Context.Roleplay;

public sealed class MapFightCountMessage : DofusMessage
{
	public new const uint StaticProtocolId = 210;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static MapFightCountMessage Empty =>
		new() { FightCount = 0 };

	public required short FightCount { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteShort(FightCount);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		FightCount = reader.ReadShort();
	}
}
