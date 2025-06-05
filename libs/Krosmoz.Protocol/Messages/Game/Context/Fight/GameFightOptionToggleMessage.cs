// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Context.Fight;

public sealed class GameFightOptionToggleMessage : DofusMessage
{
	public new const uint StaticProtocolId = 707;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static GameFightOptionToggleMessage Empty =>
		new() { Option = 0 };

	public required sbyte Option { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteInt8(Option);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		Option = reader.ReadInt8();
	}
}
