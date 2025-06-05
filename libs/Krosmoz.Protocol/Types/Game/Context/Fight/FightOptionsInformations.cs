// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Types.Game.Context.Fight;

public sealed class FightOptionsInformations : DofusType
{
	public new const ushort StaticProtocolId = 20;

	public override ushort ProtocolId =>
		StaticProtocolId;

	public static FightOptionsInformations Empty =>
		new() { IsSecret = false, IsRestrictedToPartyOnly = false, IsClosed = false, IsAskingForHelp = false };

	public required bool IsSecret { get; set; }

	public required bool IsRestrictedToPartyOnly { get; set; }

	public required bool IsClosed { get; set; }

	public required bool IsAskingForHelp { get; set; }

	public override void Serialize(BigEndianWriter writer)
	{
		var flag = new byte();
		flag = BooleanByteWrapper.SetFlag(flag, 0, IsSecret);
		flag = BooleanByteWrapper.SetFlag(flag, 1, IsRestrictedToPartyOnly);
		flag = BooleanByteWrapper.SetFlag(flag, 2, IsClosed);
		flag = BooleanByteWrapper.SetFlag(flag, 3, IsAskingForHelp);
		writer.WriteUInt8(flag);
	}

	public override void Deserialize(BigEndianReader reader)
	{
		var flag = reader.ReadUInt8();
		IsSecret = BooleanByteWrapper.GetFlag(flag, 0);
		IsRestrictedToPartyOnly = BooleanByteWrapper.GetFlag(flag, 1);
		IsClosed = BooleanByteWrapper.GetFlag(flag, 2);
		IsAskingForHelp = BooleanByteWrapper.GetFlag(flag, 3);
	}
}
