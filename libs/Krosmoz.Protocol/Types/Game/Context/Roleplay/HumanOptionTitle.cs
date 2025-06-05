// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Types.Game.Context.Roleplay;

public sealed class HumanOptionTitle : HumanOption
{
	public new const ushort StaticProtocolId = 408;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static new HumanOptionTitle Empty =>
		new() { TitleId = 0, TitleParam = string.Empty };

	public required ushort TitleId { get; set; }

	public required string TitleParam { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		writer.WriteVarUInt16(TitleId);
		writer.WriteUtfPrefixedLength16(TitleParam);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		TitleId = reader.ReadVarUInt16();
		TitleParam = reader.ReadUtfPrefixedLength16();
	}
}
