// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Context.Mount;

public sealed class MountInformationInPaddockRequestMessage : DofusMessage
{
	public new const uint StaticProtocolId = 5975;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static MountInformationInPaddockRequestMessage Empty =>
		new() { MapRideId = 0 };

	public required int MapRideId { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteVarInt32(MapRideId);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		MapRideId = reader.ReadVarInt32();
	}
}
