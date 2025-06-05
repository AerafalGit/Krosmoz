// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Game.Context;

namespace Krosmoz.Protocol.Messages.Game.Atlas.Compass;

public sealed class CompassUpdatePvpSeekMessage : CompassUpdateMessage
{
	public new const uint StaticProtocolId = 6013;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static new CompassUpdatePvpSeekMessage Empty =>
		new() { Coords = MapCoordinates.Empty, Type = 0, MemberId = 0, MemberName = string.Empty };

	public required uint MemberId { get; set; }

	public required string MemberName { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		writer.WriteVarUInt32(MemberId);
		writer.WriteUtfPrefixedLength16(MemberName);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		MemberId = reader.ReadVarUInt32();
		MemberName = reader.ReadUtfPrefixedLength16();
	}
}
