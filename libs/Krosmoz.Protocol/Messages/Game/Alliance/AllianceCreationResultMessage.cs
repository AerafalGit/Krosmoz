// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Alliance;

public sealed class AllianceCreationResultMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6391;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static AllianceCreationResultMessage Empty =>
		new() { Result = 0 };

	public required sbyte Result { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteInt8(Result);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		Result = reader.ReadInt8();
	}
}
