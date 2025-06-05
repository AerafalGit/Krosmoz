// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Game.Prism;

namespace Krosmoz.Protocol.Messages.Game.Prism;

public sealed class PrismsInfoValidMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6451;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static PrismsInfoValidMessage Empty =>
		new() { Fights = [] };

	public required IEnumerable<PrismFightersInformation> Fights { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		var fightsBefore = writer.Position;
		var fightsCount = 0;
		writer.WriteInt16(0);
		foreach (var item in Fights)
		{
			item.Serialize(writer);
			fightsCount++;
		}
		var fightsAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, fightsBefore);
		writer.WriteInt16((short)fightsCount);
		writer.Seek(SeekOrigin.Begin, fightsAfter);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		var fightsCount = reader.ReadInt16();
		var fights = new PrismFightersInformation[fightsCount];
		for (var i = 0; i < fightsCount; i++)
		{
			var entry = PrismFightersInformation.Empty;
			entry.Deserialize(reader);
			fights[i] = entry;
		}
		Fights = fights;
	}
}
