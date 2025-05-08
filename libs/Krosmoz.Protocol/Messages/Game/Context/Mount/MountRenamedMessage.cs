// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Context.Mount;

public sealed class MountRenamedMessage : DofusMessage
{
	public new const uint StaticProtocolId = 5983;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static MountRenamedMessage Empty =>
		new() { MountId = 0, Name = string.Empty };

	public required double MountId { get; set; }

	public required string Name { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteDouble(MountId);
		writer.WriteUtfLengthPrefixed16(Name);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		MountId = reader.ReadDouble();
		Name = reader.ReadUtfLengthPrefixed16();
	}
}
