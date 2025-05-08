// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Context.Roleplay.Lockable;

public class LockableStateUpdateAbstractMessage : DofusMessage
{
	public new const uint StaticProtocolId = 5671;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static LockableStateUpdateAbstractMessage Empty =>
		new() { Locked = false };

	public required bool Locked { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteBoolean(Locked);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		Locked = reader.ReadBoolean();
	}
}
