// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Game.Mount;

namespace Krosmoz.Protocol.Messages.Game.Inventory.Exchanges;

public sealed class ExchangeMountsPaddockAddMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6561;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static ExchangeMountsPaddockAddMessage Empty =>
		new() { MountDescription = [] };

	public required IEnumerable<MountClientData> MountDescription { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		var mountDescriptionBefore = writer.Position;
		var mountDescriptionCount = 0;
		writer.WriteInt16(0);
		foreach (var item in MountDescription)
		{
			item.Serialize(writer);
			mountDescriptionCount++;
		}
		var mountDescriptionAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, mountDescriptionBefore);
		writer.WriteInt16((short)mountDescriptionCount);
		writer.Seek(SeekOrigin.Begin, mountDescriptionAfter);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		var mountDescriptionCount = reader.ReadInt16();
		var mountDescription = new MountClientData[mountDescriptionCount];
		for (var i = 0; i < mountDescriptionCount; i++)
		{
			var entry = MountClientData.Empty;
			entry.Deserialize(reader);
			mountDescription[i] = entry;
		}
		MountDescription = mountDescription;
	}
}
