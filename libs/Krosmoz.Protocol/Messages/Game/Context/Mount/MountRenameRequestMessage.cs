// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Context.Mount;

public sealed class MountRenameRequestMessage : DofusMessage
{
	public new const uint StaticProtocolId = 5987;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static MountRenameRequestMessage Empty =>
		new() { Name = string.Empty, MountId = 0 };

	public required string Name { get; set; }

	public required int MountId { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteUtfPrefixedLength16(Name);
		writer.WriteVarInt32(MountId);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		Name = reader.ReadUtfPrefixedLength16();
		MountId = reader.ReadVarInt32();
	}
}
