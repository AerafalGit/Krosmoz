// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Tinsel;

public sealed class TitleSelectRequestMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6365;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static TitleSelectRequestMessage Empty =>
		new() { TitleId = 0 };

	public required ushort TitleId { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteVarUInt16(TitleId);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		TitleId = reader.ReadVarUInt16();
	}
}
