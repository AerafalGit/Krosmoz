// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Context.Roleplay.Lockable;

public sealed class LockableShowCodeDialogMessage : DofusMessage
{
	public new const uint StaticProtocolId = 5740;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static LockableShowCodeDialogMessage Empty =>
		new() { ChangeOrUse = false, CodeSize = 0 };

	public required bool ChangeOrUse { get; set; }

	public required sbyte CodeSize { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteBoolean(ChangeOrUse);
		writer.WriteInt8(CodeSize);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		ChangeOrUse = reader.ReadBoolean();
		CodeSize = reader.ReadInt8();
	}
}
