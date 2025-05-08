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

	public required double MountUid { get; set; }

	public required sbyte MountLocation { get; set; }

	public required int MountFoodUid { get; set; }

	public required int Quantity { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteDouble(MountUid);
		writer.WriteSByte(MountLocation);
		writer.WriteInt(MountFoodUid);
		writer.WriteInt(Quantity);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		MountUid = reader.ReadDouble();
		MountLocation = reader.ReadSByte();
		MountFoodUid = reader.ReadInt();
		Quantity = reader.ReadInt();
	}
}
