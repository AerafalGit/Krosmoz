// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Game.Data.Items;

namespace Krosmoz.Protocol.Messages.Game.Inventory.Items;

public sealed class MimicryObjectPreviewMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6458;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static MimicryObjectPreviewMessage Empty =>
		new() { Result = ObjectItem.Empty };

	public required ObjectItem Result { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		Result.Serialize(writer);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		Result = ObjectItem.Empty;
		Result.Deserialize(reader);
	}
}
