// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Game.Data.Items.Effects;

namespace Krosmoz.Protocol.Types.Game.Data.Items;

public sealed class ObjectItemNotInContainer : Item
{
	public new const ushort StaticProtocolId = 134;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static new ObjectItemNotInContainer Empty =>
		new() { ObjectGID = 0, Effects = [], ObjectUID = 0, Quantity = 0 };

	public required ushort ObjectGID { get; set; }

	public required IEnumerable<ObjectEffect> Effects { get; set; }

	public required uint ObjectUID { get; set; }

	public required uint Quantity { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		writer.WriteVarUInt16(ObjectGID);
		var effectsBefore = writer.Position;
		var effectsCount = 0;
		writer.WriteInt16(0);
		foreach (var item in Effects)
		{
			writer.WriteUInt16(item.ProtocolId);
			item.Serialize(writer);
			effectsCount++;
		}
		var effectsAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, effectsBefore);
		writer.WriteInt16((short)effectsCount);
		writer.Seek(SeekOrigin.Begin, effectsAfter);
		writer.WriteVarUInt32(ObjectUID);
		writer.WriteVarUInt32(Quantity);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		ObjectGID = reader.ReadVarUInt16();
		var effectsCount = reader.ReadInt16();
		var effects = new ObjectEffect[effectsCount];
		for (var i = 0; i < effectsCount; i++)
		{
			var entry = Types.TypeFactory.CreateType<ObjectEffect>(reader.ReadUInt16());
			entry.Deserialize(reader);
			effects[i] = entry;
		}
		Effects = effects;
		ObjectUID = reader.ReadVarUInt32();
		Quantity = reader.ReadVarUInt32();
	}
}
