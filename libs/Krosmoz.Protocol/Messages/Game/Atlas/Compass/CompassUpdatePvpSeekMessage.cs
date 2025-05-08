// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Atlas.Compass;

public sealed class CompassUpdatePvpSeekMessage : CompassUpdateMessage
{
	public new const uint StaticProtocolId = 6013;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static new CompassUpdatePvpSeekMessage Empty =>
		new() { WorldY = 0, WorldX = 0, Type = 0, MemberId = 0, MemberName = string.Empty };

	public required int MemberId { get; set; }

	public required string MemberName { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		writer.WriteInt(MemberId);
		writer.WriteUtfLengthPrefixed16(MemberName);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		MemberId = reader.ReadInt();
		MemberName = reader.ReadUtfLengthPrefixed16();
	}
}
