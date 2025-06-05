// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Types.Game.Paddock;

public sealed class PaddockContentInformations : PaddockInformations
{
	public new const ushort StaticProtocolId = 183;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static new PaddockContentInformations Empty =>
		new() { MaxItems = 0, MaxOutdoorMount = 0, PaddockId = 0, WorldX = 0, WorldY = 0, MapId = 0, SubAreaId = 0, Abandonned = false, MountsInformations = [] };

	public required int PaddockId { get; set; }

	public required short WorldX { get; set; }

	public required short WorldY { get; set; }

	public required int MapId { get; set; }

	public required ushort SubAreaId { get; set; }

	public required bool Abandonned { get; set; }

	public required IEnumerable<MountInformationsForPaddock> MountsInformations { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		writer.WriteInt32(PaddockId);
		writer.WriteInt16(WorldX);
		writer.WriteInt16(WorldY);
		writer.WriteInt32(MapId);
		writer.WriteVarUInt16(SubAreaId);
		writer.WriteBoolean(Abandonned);
		var mountsInformationsBefore = writer.Position;
		var mountsInformationsCount = 0;
		writer.WriteInt16(0);
		foreach (var item in MountsInformations)
		{
			item.Serialize(writer);
			mountsInformationsCount++;
		}
		var mountsInformationsAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, mountsInformationsBefore);
		writer.WriteInt16((short)mountsInformationsCount);
		writer.Seek(SeekOrigin.Begin, mountsInformationsAfter);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		PaddockId = reader.ReadInt32();
		WorldX = reader.ReadInt16();
		WorldY = reader.ReadInt16();
		MapId = reader.ReadInt32();
		SubAreaId = reader.ReadVarUInt16();
		Abandonned = reader.ReadBoolean();
		var mountsInformationsCount = reader.ReadInt16();
		var mountsInformations = new MountInformationsForPaddock[mountsInformationsCount];
		for (var i = 0; i < mountsInformationsCount; i++)
		{
			var entry = MountInformationsForPaddock.Empty;
			entry.Deserialize(reader);
			mountsInformations[i] = entry;
		}
		MountsInformations = mountsInformations;
	}
}
