// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Alliance;

public sealed class AllianceKickRequestMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6400;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static AllianceKickRequestMessage Empty =>
		new() { KickedId = 0 };

	public required uint KickedId { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteVarUInt32(KickedId);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		KickedId = reader.ReadVarUInt32();
	}
}
