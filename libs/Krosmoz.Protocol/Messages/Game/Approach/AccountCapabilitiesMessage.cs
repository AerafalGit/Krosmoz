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
		new() { TutorialAvailable = false, CanCreateNewCharacter = false, AccountId = 0, BreedsVisible = 0, BreedsAvailable = 0, Status = 0 };

	public required bool TutorialAvailable { get; set; }

	public required bool CanCreateNewCharacter { get; set; }

	public required int AccountId { get; set; }

	public required ushort BreedsVisible { get; set; }

	public required ushort BreedsAvailable { get; set; }

	public required sbyte Status { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		var flag = new byte();
		flag = BooleanByteWrapper.SetFlag(flag, 0, TutorialAvailable);
		flag = BooleanByteWrapper.SetFlag(flag, 1, CanCreateNewCharacter);
		writer.WriteUInt8(flag);
		writer.WriteInt32(AccountId);
		writer.WriteUInt16(BreedsVisible);
		writer.WriteUInt16(BreedsAvailable);
		writer.WriteInt8(Status);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		var flag = reader.ReadUInt8();
		TutorialAvailable = BooleanByteWrapper.GetFlag(flag, 0);
		CanCreateNewCharacter = BooleanByteWrapper.GetFlag(flag, 1);
		AccountId = reader.ReadInt32();
		BreedsVisible = reader.ReadUInt16();
		BreedsAvailable = reader.ReadUInt16();
		Status = reader.ReadInt8();
	}
}
