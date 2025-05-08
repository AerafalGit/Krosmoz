// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Atlas.Compass;

public sealed class CompassUpdatePartyMemberMessage : CompassUpdateMessage
{
	public new const uint StaticProtocolId = 5589;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static new CompassUpdatePartyMemberMessage Empty =>
		new() { WorldY = 0, WorldX = 0, Type = 0, MemberId = 0 };

	public required int MemberId { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		writer.WriteInt(MemberId);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		MemberId = reader.ReadInt();
	}
}
