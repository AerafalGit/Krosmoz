// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Game.Mount;

namespace Krosmoz.Protocol.Messages.Game.Inventory.Exchanges;

public class ExchangeStartOkMountWithOutPaddockMessage : DofusMessage
{
	public new const uint StaticProtocolId = 5991;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static ExchangeStartOkMountWithOutPaddockMessage Empty =>
		new() { StabledMountsDescription = [] };

	public required IEnumerable<MountClientData> StabledMountsDescription { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		var stabledMountsDescriptionBefore = writer.Position;
		var stabledMountsDescriptionCount = 0;
		writer.WriteInt16(0);
		foreach (var item in StabledMountsDescription)
		{
			item.Serialize(writer);
			stabledMountsDescriptionCount++;
		}
		var stabledMountsDescriptionAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, stabledMountsDescriptionBefore);
		writer.WriteInt16((short)stabledMountsDescriptionCount);
		writer.Seek(SeekOrigin.Begin, stabledMountsDescriptionAfter);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		var stabledMountsDescriptionCount = reader.ReadInt16();
		var stabledMountsDescription = new MountClientData[stabledMountsDescriptionCount];
		for (var i = 0; i < stabledMountsDescriptionCount; i++)
		{
			var entry = MountClientData.Empty;
			entry.Deserialize(reader);
			stabledMountsDescription[i] = entry;
		}
		StabledMountsDescription = stabledMountsDescription;
	}
}
