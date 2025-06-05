// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Game.Paddock;

namespace Krosmoz.Protocol.Messages.Game.Context.Mount;

public sealed class GameDataPaddockObjectAddMessage : DofusMessage
{
	public new const uint StaticProtocolId = 5990;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static GameDataPaddockObjectAddMessage Empty =>
		new() { PaddockItemDescription = PaddockItem.Empty };

	public required PaddockItem PaddockItemDescription { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		PaddockItemDescription.Serialize(writer);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		PaddockItemDescription = PaddockItem.Empty;
		PaddockItemDescription.Deserialize(reader);
	}
}
