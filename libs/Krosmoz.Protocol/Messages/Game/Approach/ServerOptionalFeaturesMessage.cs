// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Approach;

public sealed class ServerOptionalFeaturesMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6305;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static ServerOptionalFeaturesMessage Empty =>
		new() { Features = [] };

	public required IEnumerable<sbyte> Features { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		var featuresBefore = writer.Position;
		var featuresCount = 0;
		writer.WriteInt16(0);
		foreach (var item in Features)
		{
			writer.WriteInt8(item);
			featuresCount++;
		}
		var featuresAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, featuresBefore);
		writer.WriteInt16((short)featuresCount);
		writer.Seek(SeekOrigin.Begin, featuresAfter);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		var featuresCount = reader.ReadInt16();
		var features = new sbyte[featuresCount];
		for (var i = 0; i < featuresCount; i++)
		{
			features[i] = reader.ReadInt8();
		}
		Features = features;
	}
}
