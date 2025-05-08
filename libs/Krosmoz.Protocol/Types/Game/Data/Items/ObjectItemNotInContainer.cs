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

	public required short ObjectGID { get; set; }

	public required IEnumerable<ObjectEffect> Effects { get; set; }

	public required int ObjectUID { get; set; }

	public required int Quantity { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		writer.WriteShort(ObjectGID);
		var effectsBefore = writer.Position;
		var effectsCount = 0;
		writer.WriteShort(0);
		foreach (var item in Effects)
		{
			writer.WriteUShort(item.ProtocolId);
			item.Serialize(writer);
			effectsCount++;
		}
		var effectsAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, effectsBefore);
		writer.WriteShort((short)effectsCount);
		writer.Seek(SeekOrigin.Begin, effectsAfter);
		writer.WriteInt(ObjectUID);
		writer.WriteInt(Quantity);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		ObjectGID = reader.ReadShort();
		var effectsCount = reader.ReadShort();
		var effects = new ObjectEffect[effectsCount];
		for (var i = 0; i < effectsCount; i++)
		{
			var entry = Types.TypeFactory.CreateType<ObjectEffect>(reader.ReadUShort());
			entry.Deserialize(reader);
			effects[i] = entry;
		}
		Effects = effects;
		ObjectUID = reader.ReadInt();
		Quantity = reader.ReadInt();
	}
}
