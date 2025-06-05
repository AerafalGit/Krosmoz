// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Context.Mount;

public sealed class MountInformationRequestMessage : DofusMessage
{
	public new const uint StaticProtocolId = 5972;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static MountInformationRequestMessage Empty =>
		new() { Id = 0, Time = 0 };

	public required double Id { get; set; }

	public required double Time { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteDouble(Id);
		writer.WriteDouble(Time);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		Id = reader.ReadDouble();
		Time = reader.ReadDouble();
	}
}
