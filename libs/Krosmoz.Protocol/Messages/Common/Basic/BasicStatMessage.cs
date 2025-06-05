// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Common.Basic;

public sealed class BasicStatMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6530;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static BasicStatMessage Empty =>
		new() { StatId = 0 };

	public required ushort StatId { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteVarUInt16(StatId);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		StatId = reader.ReadVarUInt16();
	}
}
