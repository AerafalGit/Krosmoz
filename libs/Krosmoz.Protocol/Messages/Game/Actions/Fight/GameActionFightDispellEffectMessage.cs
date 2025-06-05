// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Actions.Fight;

public class GameActionFightDispellEffectMessage : GameActionFightDispellMessage
{
	public new const uint StaticProtocolId = 6113;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static new GameActionFightDispellEffectMessage Empty =>
		new() { SourceId = 0, ActionId = 0, TargetId = 0, BoostUID = 0 };

	public required int BoostUID { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		base.Serialize(writer);
		writer.WriteInt32(BoostUID);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		base.Deserialize(reader);
		BoostUID = reader.ReadInt32();
	}
}
