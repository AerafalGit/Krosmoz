// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Types.Game.Paddock;

public sealed class MountInformationsForPaddock : DofusType
{
	public new const ushort StaticProtocolId = 184;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static MountInformationsForPaddock Empty =>
		new() { ModelId = 0, Name = string.Empty, OwnerName = string.Empty };

	public required sbyte ModelId { get; set; }

	public required string Name { get; set; }

	public required string OwnerName { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteInt8(ModelId);
		writer.WriteUtfPrefixedLength16(Name);
		writer.WriteUtfPrefixedLength16(OwnerName);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		ModelId = reader.ReadInt8();
		Name = reader.ReadUtfPrefixedLength16();
		OwnerName = reader.ReadUtfPrefixedLength16();
	}
}
