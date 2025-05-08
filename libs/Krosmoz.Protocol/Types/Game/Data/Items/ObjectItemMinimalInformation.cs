// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Game.Data.Items.Effects;

namespace Krosmoz.Protocol.Types.Game.Data.Items;

public class ObjectItemMinimalInformation : Item
{
	public new const ushort StaticProtocolId = 124;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static new ObjectItemMinimalInformation Empty =>
		new() { ObjectGID = 0, Effects = [] };

	public required short ObjectGID { get; set; }

	public required IEnumerable<ObjectEffect> Effects { get; set; }

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
	}
}
