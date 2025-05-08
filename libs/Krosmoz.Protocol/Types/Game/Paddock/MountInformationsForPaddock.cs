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

	public required int ModelId { get; set; }

	public required string Name { get; set; }

	public required string OwnerName { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteInt(ModelId);
		writer.WriteUtfLengthPrefixed16(Name);
		writer.WriteUtfLengthPrefixed16(OwnerName);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		ModelId = reader.ReadInt();
		Name = reader.ReadUtfLengthPrefixed16();
		OwnerName = reader.ReadUtfLengthPrefixed16();
	}
}
