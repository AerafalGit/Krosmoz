// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Types.Game.Context.Roleplay;

public sealed class HumanOptionOrnament : HumanOption
{
	public new const ushort StaticProtocolId = 411;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static new HumanOptionOrnament Empty =>
		new() { OrnamentId = 0 };

	public required short OrnamentId { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		writer.WriteInt16(OrnamentId);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		OrnamentId = reader.ReadInt16();
	}
}
