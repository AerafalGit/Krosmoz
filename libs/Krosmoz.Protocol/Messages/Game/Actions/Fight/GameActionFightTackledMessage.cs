// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Actions.Fight;

public sealed class GameActionFightTackledMessage : AbstractGameActionMessage
{
	public new const uint StaticProtocolId = 1004;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static new GameActionFightTackledMessage Empty =>
		new() { SourceId = 0, ActionId = 0, TacklersIds = [] };

	public required IEnumerable<int> TacklersIds { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		var tacklersIdsBefore = writer.Position;
		var tacklersIdsCount = 0;
		writer.WriteInt16(0);
		foreach (var item in TacklersIds)
		{
			writer.WriteInt32(item);
			tacklersIdsCount++;
		}
		var tacklersIdsAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, tacklersIdsBefore);
		writer.WriteInt16((short)tacklersIdsCount);
		writer.Seek(SeekOrigin.Begin, tacklersIdsAfter);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		var tacklersIdsCount = reader.ReadInt16();
		var tacklersIds = new int[tacklersIdsCount];
		for (var i = 0; i < tacklersIdsCount; i++)
		{
			tacklersIds[i] = reader.ReadInt32();
		}
		TacklersIds = tacklersIds;
	}
}
