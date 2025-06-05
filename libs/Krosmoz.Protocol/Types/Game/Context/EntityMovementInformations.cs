// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Types.Game.Context;

public sealed class EntityMovementInformations : DofusType
{
	public new const ushort StaticProtocolId = 63;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static EntityMovementInformations Empty =>
		new() { Id = 0, Steps = [] };

	public required int Id { get; set; }

	public required IEnumerable<sbyte> Steps { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteInt32(Id);
		var stepsBefore = writer.Position;
		var stepsCount = 0;
		writer.WriteInt16(0);
		foreach (var item in Steps)
		{
			writer.WriteInt8(item);
			stepsCount++;
		}
		var stepsAfter = writer.Position;
		writer.Seek(SeekOrigin.Begin, stepsBefore);
		writer.WriteInt16((short)stepsCount);
		writer.Seek(SeekOrigin.Begin, stepsAfter);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		Id = reader.ReadInt32();
		var stepsCount = reader.ReadInt16();
		var steps = new sbyte[stepsCount];
		for (var i = 0; i < stepsCount; i++)
		{
			steps[i] = reader.ReadInt8();
		}
		Steps = steps;
	}
}
