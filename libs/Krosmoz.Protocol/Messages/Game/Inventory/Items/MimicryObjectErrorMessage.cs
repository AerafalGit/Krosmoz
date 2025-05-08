// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Inventory.Items;

public sealed class MimicryObjectErrorMessage : ObjectErrorMessage
{
	public new const uint StaticProtocolId = 6461;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static new MimicryObjectErrorMessage Empty =>
		new() { Reason = 0, Preview = false, ErrorCode = 0 };

	public required bool Preview { get; set; }

	public required sbyte ErrorCode { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		writer.WriteBoolean(Preview);
		writer.WriteSByte(ErrorCode);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		Preview = reader.ReadBoolean();
		ErrorCode = reader.ReadSByte();
	}
}
