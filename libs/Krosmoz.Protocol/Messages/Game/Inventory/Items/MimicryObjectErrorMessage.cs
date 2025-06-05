// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Inventory.Items;

public sealed class MimicryObjectErrorMessage : SymbioticObjectErrorMessage
{
	public new const uint StaticProtocolId = 6461;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static new MimicryObjectErrorMessage Empty =>
		new() { Reason = 0, ErrorCode = 0, Preview = false };

	public required bool Preview { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		writer.WriteBoolean(Preview);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		Preview = reader.ReadBoolean();
	}
}
