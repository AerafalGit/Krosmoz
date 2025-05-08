// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Packs;

public sealed class PackRestrictedSubAreaMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6186;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static PackRestrictedSubAreaMessage Empty =>
		new() { SubAreaId = 0 };

	public required int SubAreaId { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteInt(SubAreaId);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		SubAreaId = reader.ReadInt();
	}
}
