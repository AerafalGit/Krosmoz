// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Game.Mount;

namespace Krosmoz.Protocol.Messages.Game.Inventory.Exchanges;

public sealed class UpdateMountBoostMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6179;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static UpdateMountBoostMessage Empty =>
		new() { RideId = 0, BoostToUpdateList = [] };

	public required int RideId { get; set; }

	public required IEnumerable<UpdateMountBoost> BoostToUpdateList { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteVarInt32(RideId);
		var boostToUpdateListBefore = writer.Position;
		var boostToUpdateListCount = 0;
		writer.WriteInt16(0);
		foreach (var item in BoostToUpdateList)
		{
			writer.WriteUInt16(item.ProtocolId);
			item.Serialize(writer);
			boostToUpdateListCount++;
		}
		var boostToUpdateListAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, boostToUpdateListBefore);
		writer.WriteInt16((short)boostToUpdateListCount);
		writer.Seek(SeekOrigin.Begin, boostToUpdateListAfter);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		RideId = reader.ReadVarInt32();
		var boostToUpdateListCount = reader.ReadInt16();
		var boostToUpdateList = new UpdateMountBoost[boostToUpdateListCount];
		for (var i = 0; i < boostToUpdateListCount; i++)
		{
			var entry = Types.TypeFactory.CreateType<UpdateMountBoost>(reader.ReadUInt16());
			entry.Deserialize(reader);
			boostToUpdateList[i] = entry;
		}
		BoostToUpdateList = boostToUpdateList;
	}
}
