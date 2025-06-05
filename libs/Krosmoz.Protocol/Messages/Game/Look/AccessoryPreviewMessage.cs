// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.Protocol.Types.Game.Look;

namespace Krosmoz.Protocol.Messages.Game.Look;

public sealed class AccessoryPreviewMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6517;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static AccessoryPreviewMessage Empty =>
		new() { Look = EntityLook.Empty };

	public required EntityLook Look { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		Look.Serialize(writer);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		Look = EntityLook.Empty;
		Look.Deserialize(reader);
	}
}
