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

	public required short TitleId { get; set; }

	public required string TitleParam { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		writer.WriteShort(TitleId);
		writer.WriteUtfLengthPrefixed16(TitleParam);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		TitleId = reader.ReadShort();
		TitleParam = reader.ReadUtfLengthPrefixed16();
	}
}
