// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Inventory.Preset;

public sealed class InventoryPresetItemUpdateErrorMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6211;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static InventoryPresetItemUpdateErrorMessage Empty =>
		new() { Code = 0 };

	public required sbyte Code { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteInt8(Code);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		Code = reader.ReadInt8();
	}
}
