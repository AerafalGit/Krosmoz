// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Messages.Game.Approach;

public sealed class AccountCapabilitiesMessage : DofusMessage
{
	public new const uint StaticProtocolId = 6216;

	public override uint ProtocolId =>
		StaticProtocolId;

	public static AccountCapabilitiesMessage Empty =>
		new() { AccountId = 0, TutorialAvailable = false, BreedsVisible = 0, BreedsAvailable = 0, Status = 0 };

	public required int AccountId { get; set; }

	public required bool TutorialAvailable { get; set; }

	public required short BreedsVisible { get; set; }

	public required short BreedsAvailable { get; set; }

	public required sbyte Status { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		writer.WriteInt(AccountId);
		writer.WriteBoolean(TutorialAvailable);
		writer.WriteShort(BreedsVisible);
		writer.WriteShort(BreedsAvailable);
		writer.WriteSByte(Status);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		AccountId = reader.ReadInt();
		TutorialAvailable = reader.ReadBoolean();
		BreedsVisible = reader.ReadShort();
		BreedsAvailable = reader.ReadShort();
		Status = reader.ReadSByte();
	}
}
