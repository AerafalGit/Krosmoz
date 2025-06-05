// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Context.Mount;

public sealed class MountFeedRequestMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6189;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static MountFeedRequestMessage Empty =>
		new() { MountUid = 0, MountLocation = 0, MountFoodUid = 0, Quantity = 0 };

	public required uint MountUid { get; set; }

	public required sbyte MountLocation { get; set; }

	public required uint MountFoodUid { get; set; }

	public required uint Quantity { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteVarUInt32(MountUid);
		writer.WriteInt8(MountLocation);
		writer.WriteVarUInt32(MountFoodUid);
		writer.WriteVarUInt32(Quantity);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		MountUid = reader.ReadVarUInt32();
		MountLocation = reader.ReadInt8();
		MountFoodUid = reader.ReadVarUInt32();
		Quantity = reader.ReadVarUInt32();
	}
}
