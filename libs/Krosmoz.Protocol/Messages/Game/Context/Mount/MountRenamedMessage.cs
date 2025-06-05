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

	public required int MountId { get; set; }

	public required string Name { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteVarInt32(MountId);
		writer.WriteUtfPrefixedLength16(Name);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		MountId = reader.ReadVarInt32();
		Name = reader.ReadUtfPrefixedLength16();
	}
}
