// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Context;

public sealed class ShowCellSpectatorMessage : ShowCellMessage
{
	public new const uint StaticProtocolId = 6158;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static new ShowCellSpectatorMessage Empty =>
		new() { CellId = 0, SourceId = 0, PlayerName = string.Empty };

	public required string PlayerName { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		writer.WriteUtfPrefixedLength16(PlayerName);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		PlayerName = reader.ReadUtfPrefixedLength16();
	}
}
